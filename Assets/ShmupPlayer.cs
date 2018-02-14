using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShmupPlayer : ShmupEntity {

    public float speed;
    public float currentRotation;

    public int maxHealth;
    public int health;
    public int bombs;

    public float bulletSpeed;
    public float rateOfFire;
    private float cooldown;
    private float bombCooldown;

    private float bombCooldownDuration = 2;


    //External GameObjects
    public GameObject bulletPrefab;
    public GameObject bombPrefab;

    public bool inGrazeForm;

    //Models
    public GameObject normalForm;
    public GameObject grazeForm;

    //Components
    private Rigidbody selfBody;


	// Use this for initialization
	void Start () {
        this.selfBody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if (inGrazeForm)
            return;

        //Movement
        Vector2 moveDir = Controls.getDirection();
        this.selfBody.velocity = (Vector3.right * moveDir.x + Vector3.forward*moveDir.y) * speed;

        //Rotation
        Vector2 aimDir = Controls.getAimDirection();
        float pendingRotation = 0.0f;
        if(aimDir.magnitude >= 0.8)
        {
            pendingRotation = Mathf.Atan(aimDir.x / aimDir.y) * Mathf.Rad2Deg;
            if (aimDir.y < 0)
                pendingRotation += 180;
        }
        else
        {
            pendingRotation = Mathf.Atan(moveDir.x / moveDir.y) * Mathf.Rad2Deg;
            if (moveDir.y < 0)
                pendingRotation += 180;
        }

        if (!float.IsNaN(pendingRotation))
            currentRotation = pendingRotation;

        currentRotation = currentRotation % 360;
        this.transform.eulerAngles = Vector3.up * currentRotation;

        //Shooting
        cooldown -= Time.deltaTime;
        if (Controls.shootInputHeld() && cooldown <= 0.0f)
        {
            cooldown = 1 / rateOfFire;
            ShootBullet();
        }


        bombCooldown -= Time.deltaTime;
        if(Controls.bombInputDown() && bombs > 0 && bombCooldown <= 0.0f)
        {
            bombCooldown = bombCooldownDuration;

            bombs--;
            UseBomb();
        }

        //Did we die?
        if (health <= 0)
            StartCoroutine(this.Die());
    }

    void ShootBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.hitbox.owner = this.gameObject;
        bullet.gameObject.transform.position = this.gameObject.transform.position;

        float xDir = Mathf.Sin(Mathf.Deg2Rad * currentRotation);
        float yDir = Mathf.Cos(Mathf.Deg2Rad * currentRotation);
        bullet.Fire(xDir, yDir, bulletSpeed);
    }

    void UseBomb()
    {
        Bomb bomb = Instantiate(bombPrefab).GetComponent<Bomb>();
        bomb.hitbox.owner = this.gameObject;
        bomb.gameObject.transform.position = this.gameObject.transform.position;
    }

    public override void OnHit(float damage)
    {
        this.health -= 1;
    }

    public void GainBomb()
    {
        this.bombs++;
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(2.0f);

        ShmupGameManager.instance.RespawnPlayer();

        yield return null;
    }

    public void Materialize()
    {
        this.normalForm.SetActive(true);
        this.grazeForm.SetActive(false);
        this.inGrazeForm = false;
    }

    public void Graze()
    {
        this.normalForm.SetActive(false);
        this.grazeForm.SetActive(true);
        this.inGrazeForm = true;
    }


    private int startingBombs = 3;
    public void Spawn(SpawnPoint spawn)
    {
        this.health = maxHealth;
        this.bombs = startingBombs;
        this.transform.position = spawn.transform.position;
    }
}
