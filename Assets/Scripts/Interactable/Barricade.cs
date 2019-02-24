using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : ShmupEntity, ShmupSpawnable {

    public int hackingThreshold;
    public int hackingProgress;

    public GameObject model;
    public GameObject selfCollider;

    private bool destroyed;

    public override void OnHit(float damage)
    {
        hackingProgress++;

        if (hackingProgress >= hackingThreshold)
        {
            hackingProgress = 0;
            Die();
        }
    }

    public override void OnStun()
    {
        throw new System.NotImplementedException();
    }

    public void Spawn()
    {
        model.SetActive(true);
        selfCollider.SetActive(true);
        destroyed = false;
    }

    public void Die()
    {
        destroyed = true;
        model.SetActive(false);
        selfCollider.SetActive(false);
    }

    public override bool IsCompleted()
    {
        return destroyed;
    }

    public override void Suspend()
    {
        return;
    }

    public override void Unsuspend()
    {
        return;
    }

    public bool IsDead()
    {
        return destroyed;
    }
}
