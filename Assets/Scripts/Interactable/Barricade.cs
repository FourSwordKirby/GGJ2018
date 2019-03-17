using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : ShmupEntity, ShmupSpawnable {

    public int hackingThreshold;
    public int hackingProgress;

    public GameObject model;
    public GameObject selfHurtbox;
    public Collider ECB;

    public Material baseMaterial;
    public Material dissolveMaterial;
    public MeshRenderer meshRenderer;

    private bool destroyed;

    public override void OnHit(float damage)
    {
        hackingProgress += (int)damage;

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
        selfHurtbox.SetActive(true);
        destroyed = false;
        StartCoroutine(MaterializeAnim());
    }

    private IEnumerator MaterializeAnim()
    {
        float dissolveTime = 0.5f;
        float animTimer = dissolveTime;
        meshRenderer.material = dissolveMaterial;
        while (animTimer > 0)
        {
            animTimer -= Time.deltaTime;
            meshRenderer.material.SetFloat("_DissolveIntensity", (animTimer / dissolveTime));
            yield return null;
        }

        meshRenderer.material = baseMaterial;
        yield return null;
    }

    public void Die()
    {
        SfxController.instance.PlaySound("Barricade Die");

        destroyed = true;
        selfHurtbox.SetActive(false);
        ECB.enabled = false;
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
