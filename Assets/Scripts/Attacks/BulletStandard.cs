using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStandard : Bullet
{
    public float durability;
    public int damage;
    public float duration;
    public GameObject model;

    public bool isPiercing;

    Rigidbody selfBody;
    bool dead;

    private void Awake()
    {
        this.selfBody = this.GetComponent<Rigidbody>();
        this.GetComponent<ShmupHitbox3D>().destroyEffect = (() => OnHit(1000));
    }

    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0 && !dead)
        {
            dead = true;
            StartCoroutine(Die());
        }
    }

    public override void Fire(float xDir, float yDir, float speed)
    {
        this.selfBody.velocity = (Vector3.right * xDir + Vector3.forward * yDir) * speed;
        this.gameObject.transform.eulerAngles = Vector2.up * Mathf.Atan(xDir / yDir) * Mathf.Rad2Deg;
    }

    private IEnumerator Die()
    {
        Destroy(this.gameObject);
        yield return null;
    }

    public override void OnHit(float damage)
    {
        AudioManager.instance.OnHitEnemyShot();

        if (!isPiercing)
        {
            this.durability -= damage;
            if (this.durability <= 0 && !dead)
            {
                dead = true;
                StartCoroutine(Die());
            }
        }
    }

    public override void OnStun()
    {
        if(!dead)
        {
            dead = true;
            StartCoroutine(Die());
        }
    }

    public override bool IsCompleted()
    {
        throw new System.NotImplementedException();
    }


    public override void Suspend()
    {
        return;
    }

    public override void Unsuspend()
    {
        return;
    }

    /* When the game is paused manually, bullets are stopped from moving by making time.deltatime = 0
    Vector3 bulletVelocity;
    public override void Suspend()
    {
        bulletVelocity = this.selfBody.velocity;
        this.selfBody.velocity = Vector3.zero;
    }

    public override void Unsuspend()
    {
        this.selfBody.velocity = bulletVelocity;
    }
    */
}
