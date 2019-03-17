using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Barrier : ShmupEntity
{
    public List<SecurityLock> locks;
    public GameObject model;

    public Rigidbody signalBeam;

    private void Start()
    {
        foreach(SecurityLock lockObj in locks)
        {
            lockObj.AddLinkedObject(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        model.SetActive(!locks.All(x => x.unlocked));
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    public float cooldown;
    public float cooldownTimer;
    public override void OnHit(float damage)
    {
        if (cooldownTimer > 0)
        {
            return;
        }
        else
        {
            foreach(Vector3 pos in locks.Select(x => x.transform.position))
            {
                EffectManager.instance.SpawnLinkSignal(transform.position, pos);
            }
        }
    }

    public bool isClosed()
    {
        return model.activeSelf;
    }

    public override void OnStun()
    {
        throw new System.NotImplementedException();
    }

    public void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsCompleted()
    {
        throw new System.NotImplementedException();
    }

    public override void Suspend()
    {
    }

    public override void Unsuspend()
    {
        //throw new System.NotImplementedException();
    }
}
