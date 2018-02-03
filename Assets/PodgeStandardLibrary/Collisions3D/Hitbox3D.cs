using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Hitbox3D : Collisionbox{
    public GameObject owner;
    public float damage;
    public bool persistent;
}
