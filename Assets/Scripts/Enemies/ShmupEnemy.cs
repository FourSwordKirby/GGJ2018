using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShmupEnemy : ShmupEntity{
    public abstract void Spawn();
    public abstract void Die();
}
