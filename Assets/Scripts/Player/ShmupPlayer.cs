using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShmupPlayer : ShmupEntity, ShmupSpawnable {

    public float speed;
    public float targetRotation;
    public float currentRotation;

    public int maxHealth;
    public int health;

    public int maxBombCharge;
    public int startingBombs;
    public int bombCharge;

    public float bulletSpeed;
    public float rateOfFire;
    private float cooldown;
    private float bombCooldown;

    //Constants?
    private float bombCooldownDuration = 2;
    private float turnSpeed = 30f;


    //External GameObjects
    public GameObject firingOrigin;
    public GameObject bulletPrefab;
    public BombSpawner bombSpawner;

    public bool inGrazeForm;

    //Models
    public GameObject normalForm;
    public GameObject grazeForm;

    //Animations
    public ParticleSystem DeathEffect1;
    public ParticleSystem DeathEffect2;
    public ParticleSystem DeathEffect3;

    //Components
    private Rigidbody selfBody;

    public const int MIN_BOMB_CHARGE = 100;


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
        if(aimDir.magnitude >= 0.4)
        {
            pendingRotation = Mathf.Atan(aimDir.x / aimDir.y) * Mathf.Rad2Deg;
            if (aimDir.y < 0)
                pendingRotation += 180;
            if (!float.IsNaN(pendingRotation))
                targetRotation = pendingRotation;
        }

        currentRotation = this.transform.eulerAngles.y;
        targetRotation = (targetRotation % 360 + 360) % 360;
        if(Mathf.Abs(targetRotation - currentRotation) > 180)
        {
            float absoluteRotation = (currentRotation % 360 + 360) % 360;
            if (absoluteRotation > 180)
                currentRotation = absoluteRotation - 360.0f;
            else
                targetRotation = targetRotation - 360.0f;
        }
        //this.transform.localRotation = Quaternion.Euler(Vector3.up * Mathf.MoveTowards(currentRotation, targetRotation, turnSpeed));
        this.selfBody.rotation = Quaternion.Euler(Vector3.up * Mathf.MoveTowards(currentRotation, targetRotation, turnSpeed));
        //this.transform.localRotation = this.selfBody.rotation;

        //Shooting
        cooldown -= Time.deltaTime;
        if (Controls.shootInputHeld() && cooldown <= 0.0f)
        {
            cooldown = 1 / rateOfFire;
            ShootBullet();
        }


        bombCooldown -= Time.deltaTime;
        if(Controls.bombInputDown() && bombCharge >= MIN_BOMB_CHARGE && bombCooldown <= 0.0f)
        {
            bombCooldown = bombCooldownDuration;

            bombCharge-= MIN_BOMB_CHARGE;
            UseBomb();
        }
    }

    void ShootBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.hitbox.isPlayerOwned = true;
        bullet.hurtbox.isPlayerOwned = true;
        bullet.hitbox.owner = this.gameObject;

        float xDir = Mathf.Sin(Mathf.Deg2Rad * targetRotation);
        float yDir = Mathf.Cos(Mathf.Deg2Rad * targetRotation);

        bullet.gameObject.transform.position = firingOrigin.transform.position;

        bullet.Fire(xDir, yDir, bulletSpeed);
    }

    void UseBomb()
    {
        bombSpawner.Spawn(this.transform.position);
    }

    public override void OnHit(float damage)
    {
        this.health -= (int)damage;
        if (this.health <= 0)
            this.Die();
    }

    public override void OnStun()
    {
        throw new System.NotImplementedException();
    }

    public void GainBomb(int bombValue)
    {
        this.bombCharge = Mathf.Min(bombCharge + bombValue, maxBombCharge);
    }

    private bool dying;
    private IEnumerator HandleDeath()
    {
        if (dying)
            yield break;

        dying = true;
        //Death animation happens here
        DeathEffect1.Play();
        DeathEffect2.Play();
        DeathEffect3.Play();
        this.normalForm.SetActive(false); //Hacky crap that should get handled elsewhere
        CameraControlsTopDown3D.instance.Shake(0.4f, 0.6f);
        yield return new WaitForSeconds(1.0f);
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

    public void FreezeVelocity()
    {
        this.selfBody.velocity = Vector3.zero;
    }

    public void Spawn(SpawnPoint spawn)
    {
        this.gameObject.SetActive(true);
        this.normalForm.SetActive(true); //Hacky crap that should get handled elsewhere
        this.health = maxHealth;
        this.bombCharge = Mathf.Max(startingBombs, (int)(this.bombCharge * 0.5f));
        this.transform.position = spawn.transform.position;
        dying = false;
    }

    public override bool IsCompleted()
    {
        throw new System.NotImplementedException();
    }

    public override void Suspend()
    {
        Controls.DisableGameplayControls();
    }

    public override void Unsuspend()
    {
        Controls.EnableGameplayControls();
    }

    public void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        StartCoroutine(HandleDeath());
    }

    public bool IsDead()
    {
        return this.health <= 0;
    }
}
