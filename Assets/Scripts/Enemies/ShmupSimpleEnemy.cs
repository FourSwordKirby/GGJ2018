using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This kind of enemy slowly moves towards the player and shoots at them
public class ShmupSimpleEnemy : ShmupEnemy {

    public GameObject target;
    public float currentRotation;
    public float speed;
    public float bulletSpeed;

    public float rateOfFire;
    private float cooldown;

    //External GameObjects
    public GameObject bulletPrefab;

    //Components
    private Rigidbody selfBody;

    private Vector3 spawnLocation;

    private void Awake()
    {
        this.spawnLocation = this.transform.position;
    }

    // Update is called once per frame
    void Update () {
        //Movement
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, speed);

        //Rotation
        Vector3 dirVector = this.transform.position - target.transform.position;
        currentRotation = Mathf.Atan(dirVector.x / dirVector.z) * Mathf.Rad2Deg;
        if (dirVector.z > 0)
            currentRotation += 180;
        currentRotation = currentRotation % 360;
        this.transform.eulerAngles = Vector3.up * currentRotation;

        //Shooting
        cooldown += Time.deltaTime;
        if (cooldown > 1 / rateOfFire)
        {
            cooldown = 0;
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.hitbox.isPlayerOwned = false;
        bullet.hurtbox.isPlayerOwned = false;
        bullet.gameObject.transform.position = this.gameObject.transform.position;

        float xDir = Mathf.Sin(Mathf.Deg2Rad * currentRotation);
        float yDir = Mathf.Cos(Mathf.Deg2Rad * currentRotation);
        bullet.Fire(xDir, yDir, bulletSpeed);
    }

    public override void OnHit(float damage)
    {
        this.gameObject.SetActive(false);
    }

    public override void Spawn()
    {
        this.transform.position = spawnLocation;
        this.gameObject.SetActive(true);
        //Some kind of animation and initial location fixing
    }
}
