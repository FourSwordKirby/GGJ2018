using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public Hitbox3D hitbox;
    public abstract void Fire(float xDir, float yDir, float speed);
}
