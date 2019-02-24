using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : ShmupEntity, ShmupSpawnable
{
    public Wave linkedWave;

    public Material onMaterial;
    public Material offMaterial;

    public MeshRenderer selfRenderer;
    public Collider selfCollider;

    private bool triggerEnabled;
    private bool triggerActivated;

    private void OnTriggerEnter(Collider col)
    {
        Hurtbox3D colBox = col.GetComponent<Hurtbox3D>();
        if (colBox != null && colBox.owner.GetComponent<ShmupPlayer>() != null)
        {
            triggerActivated = true;
            linkedWave.FinishWave();
            Die();
        }
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        triggerEnabled = false;
    }

    public override bool IsCompleted()
    {
        return triggerActivated;
    }

    public override void OnHit(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void OnStun()
    {
        throw new System.NotImplementedException();
    }

    public void Enable()
    {
        if (this.selfRenderer.material != onMaterial)
        {
            this.selfRenderer.material = onMaterial;
            selfCollider.enabled = true;
            triggerEnabled = true;
        }
    }

    public void Spawn()
    {
    }

    public override void Suspend()
    {
    }

    public override void Unsuspend()
    {
    }

    public bool IsDead()
    {
        return !triggerEnabled;
    }
}

