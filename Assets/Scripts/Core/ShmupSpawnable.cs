using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All Entities have health and can be hit by bullets. Some bullets can also be hit by other bullets
public interface ShmupSpawnable {
    void Spawn();
    void Die();
    bool IsDead();
}
