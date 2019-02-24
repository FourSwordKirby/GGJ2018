using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : ShmupEntity, ShmupSpawnable
{

    public Material onMaterial;
    public Material offMaterial;

    public MeshRenderer selfRenderer;

    public void Die()
    {
        this.gameObject.SetActive(false);
    }

    public override bool IsCompleted()
    {
        throw new System.NotImplementedException();
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
        this.gameObject.SetActive(true);
        /*
        if (this.selfRenderer.material != onMaterial)
        {
            this.selfRenderer.material = onMaterial;
            selfCollider.enabled = true;
            triggerEnabled = true;
        }
        */
    }

    public override void Suspend()
    {
    }

    public override void Unsuspend()
    {
    }

    public bool IsDead()
    {
        return !gameObject.activeSelf;
        //return !triggerEnabled;
    }
}
