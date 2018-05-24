﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FiringMode
{
    aim,
    spiral,
    rigid
}

public enum MovementMode
{
    tracking,
    patroling,
    bouncing,
    rigid
}

//This kind of enemy slowly moves towards the player and shoots at them
public class ShmupSimpleEnemy : ShmupEnemy {

    public float targetRotation;
    public float currentRotation;

    public float health;

    public float speed;
    public float turnSpeed;
    public float bulletSpeed;
    public int bulletStreams;
    public float bulletSpread;

    public MovementMode movementMode;
    public FiringMode firingMode;
    public GameObject target;

    public float rateOfFire;
    private float cooldown;

    public bool isEngaged;

    //External GameObjects
    public GameObject bulletPrefab;
    public GameObject pingEffect;
    public GameObject deathEffect;

    //Components
    private Vector3 spawnLocation;

    private Rigidbody selfBody;
    private Animator anim;

    private void Awake()
    {
        spawnLocation = this.transform.position;
        selfBody = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEngaged)
            return;

        //Movement
        switch (movementMode)
        {
            case MovementMode.tracking:
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, speed);
                break;
            case MovementMode.patroling:
                break;
            case MovementMode.bouncing:
                break;
            case MovementMode.rigid:
                break;
        }

        //Rotation
        Vector3 dirVector;
        switch (firingMode)
        {
            case FiringMode.aim:
                //Move this into a module maybe?
                dirVector = this.transform.position - target.transform.position;
                targetRotation = Mathf.Atan(dirVector.x / dirVector.z) * Mathf.Rad2Deg;

                if (dirVector.z > 0)
                    targetRotation += 180;

                currentRotation = this.transform.eulerAngles.y;
                targetRotation = (targetRotation % 360 + 360) % 360;

                if (Mathf.Abs(targetRotation - currentRotation) > 180)
                {
                    float absoluteRotation = (currentRotation % 360 + 360) % 360;
                    if (absoluteRotation > 180)
                        currentRotation = absoluteRotation - 360.0f;
                    else
                        targetRotation = targetRotation - 360.0f;
                }
                this.transform.eulerAngles = Vector3.up * Mathf.MoveTowards(currentRotation, targetRotation, turnSpeed);
                break;

            case FiringMode.spiral:
                currentRotation += turnSpeed;
                this.transform.eulerAngles = Vector3.up * currentRotation;
                break;
            case FiringMode.rigid:
                break;
        }

        //Shooting
        cooldown += Time.deltaTime;
        if (cooldown > 1 / rateOfFire)
        {
            cooldown = 0;
            ShootBullet(bulletStreams, bulletSpread);
        }
    }

    void ShootBullet(int bulletStreams, float bulletSpread)
    {
        float deltaAngle = bulletSpread / bulletStreams;

        for(int i = 0; i < bulletStreams; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
            bullet.hitbox.isPlayerOwned = false;
            bullet.hurtbox.isPlayerOwned = false;
            bullet.gameObject.transform.position = this.gameObject.transform.position;

            float offset = (i - bulletStreams / 2) * deltaAngle;

            if (bulletStreams % 2 == 0)
                offset += deltaAngle / 2;


            float xDir = Mathf.Sin(Mathf.Deg2Rad * (currentRotation + offset));
            float yDir = Mathf.Cos(Mathf.Deg2Rad * (currentRotation + offset));
            bullet.Fire(xDir, yDir, bulletSpeed);
        }
    }

    public override void OnHit(float damage)
    {
        //this.gameObject.SetActive(false);
        this.health -= damage;
        if (this.health <= 0)
            this.Die();
        else
            anim.SetTrigger("Hit");
    }

    public override void OnStun()
    {
        anim.SetTrigger("Stun");
        this.rateOfFire = 0;
        CameraControlsTopDown3D.instance.Shake(0.1f, 0.1f);
    }

    public override void Die()
    {
        anim.SetTrigger("Die");
        CameraControlsTopDown3D.instance.Shake(0.1f, 0.25f);
        print("Implement this properly (as in actually destroy the enemy object via animator or something");
        this.rateOfFire = 0;
    }

    public override void Spawn()
    {
        this.transform.position = spawnLocation;
        this.gameObject.SetActive(true);
    }

    private void PingEffect()
    {
        GameObject ping = Instantiate(pingEffect);
        ping.transform.position = this.transform.position + Vector3.up * 0.1f;
        ping.GetComponent<PingEffect>().duration = 0.5f;
    }
}
