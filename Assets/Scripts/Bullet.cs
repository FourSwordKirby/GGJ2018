using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : ShmupEntity
{
    public ShmupHitbox3D hitbox;
    public ShmupHitbox3D hurtbox;

    public abstract void Fire(float xDir, float yDir, float speed);
}
