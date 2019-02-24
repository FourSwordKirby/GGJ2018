using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShmupEnemy : ShmupEntity, ShmupSpawnable {
    public abstract void Spawn();
    public abstract void Die();
    public abstract bool IsDefeated();
    public abstract float GetHealth();
    public abstract float GetMaxHealth();

    public override bool IsCompleted()
    {
        return IsDefeated();
    }

    public bool IsDead()
    {
        return IsDefeated();
    }
}
