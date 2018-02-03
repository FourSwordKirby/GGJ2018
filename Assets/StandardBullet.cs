using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : Bullet
{
    public int durability;
    public int damage;
    public float duration;

    Rigidbody selfBody;
    
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
    }
}
