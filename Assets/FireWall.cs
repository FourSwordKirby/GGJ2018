using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireWall : ShmupEntity, ShmupSpawnable
{
    public ParticleSystem emitter0;
    public ParticleSystem emitter1;

    public Collider collisionBox;

    public List<GameObject> linkedGameObjects;
    private List<ShmupEntity> linkedEntities;

    void Awake()
    {
        linkedEntities = linkedGameObjects.Select(x => x.GetComponentsInChildren<ShmupEntity>().ToList()).SelectMany(x => x).ToList();
    }

    void Update()
    {
        if (IsCompleted())
            Die();
    }

    public void Die()
    {
        ParticleSystem.EmissionModule mod0 = emitter0.emission;
        mod0.rateOverTime = 0;
        ParticleSystem.EmissionModule mod1 = emitter1.emission;
        mod1.rateOverTime = 0;

        collisionBox.enabled = false;
    }

    public override bool IsCompleted()
    {
        return linkedEntities.TrueForAll(x => x == null || x.IsCompleted());
    }

    public override void OnHit(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void OnStun()
    {
        throw new System.NotImplementedException();
    }

    public void Spawn()
    {
        ParticleSystem.EmissionModule mod0 = emitter0.emission;
        mod0.rateOverTime = 18;
        ParticleSystem.EmissionModule mod1 = emitter1.emission;
        mod1.rateOverTime = 18;

        collisionBox.enabled = true;
    }

    public override void Suspend()
    {
    }

    public override void Unsuspend()
    {
    }

    public bool IsDead()
    {
        return IsCompleted();
    }
}
