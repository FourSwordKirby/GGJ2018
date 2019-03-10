using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : ShmupEntity, ShmupSpawnable {

    public int hackingThreshold;
    public int hackingProgress;

    public GameObject model;
    public GameObject selfCollider;

    public Material dissolveMaterial;
    public MeshRenderer meshRenderer;

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
        selfCollider.SetActive(false);
        StartCoroutine(DissolveAnim());
    }

    private IEnumerator DissolveAnim()
    {
        float dissolveTime = 0.7f;
        float animTimer = dissolveTime;
        meshRenderer.material = dissolveMaterial;
        while (animTimer > 0)
        {
            animTimer -= Time.deltaTime;
            meshRenderer.material.SetFloat("_DissolveIntensity", 1.0f - (animTimer / dissolveTime));
            yield return null;
        }
        model.SetActive(false);
        yield return null;
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
