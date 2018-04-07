using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStandard : Bullet
{
    public float durability;
    public int damage;
    public float duration;

    Rigidbody selfBody;

    public bool isPiercing;

    private void Awake()
    {
        this.selfBody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
            Destroy(this.gameObject);
    }

    public override void Fire(float xDir, float yDir, float speed)
    {
        this.selfBody.velocity = (Vector3.right * xDir + Vector3.forward * yDir) * speed;
        this.gameObject.transform.eulerAngles = Vector2.up * Mathf.Atan(xDir / yDir) * Mathf.Rad2Deg;
    }

    public override void OnHit(float damage)
    {
        if(!isPiercing)
        {
            this.durability -= damage;
            if (this.durability <= 0)
                Destroy(this.gameObject);
        }
    }
}
