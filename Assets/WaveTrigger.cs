using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveTrigger : ShmupEntity, ShmupSpawnable
{
    public Wave linkedWave;

    public Material onMaterial;
    public Material offMaterial;

    public MeshRenderer selfRenderer;
    public Collider selfCollider;

    public GameObject LightEffect;

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
        if (!triggerEnabled)
        {
            print(this.gameObject.transform.parent);
            this.selfRenderer.material = onMaterial;
            selfCollider.enabled = true;
            triggerEnabled = true;
            StartCoroutine(TriggerActiveAnimation());
        }
    }

    public IEnumerator TriggerActiveAnimation()
    {
        float animTimer = 0.0f;
        while (animTimer < 0.6f)
        {
            animTimer += Time.deltaTime;
            float yScale = Mathf.Lerp(5.0f, 0.5f, animTimer / 0.6f);
            LightEffect.transform.localScale = new Vector3(LightEffect.transform.localScale.x, yScale, LightEffect.transform.localScale.z);
            yield return null;
        }
        LightEffect.GetComponentsInChildren<MeshCollider>().ToList().ForEach(x => x.enabled = false);

        yield return null;
    }

    public void Spawn()
    {
        print("called Trigger spawn");
        StartCoroutine(SpawnAnimation());
    }

    public IEnumerator SpawnAnimation()
    {
        float animTimer = 0.0f;
        while (animTimer < 0.6f)
        {
            animTimer += Time.deltaTime;
            float yScale = Mathf.Lerp(0.5f, 5.0f, animTimer / 0.6f);
            LightEffect.transform.localScale = new Vector3(LightEffect.transform.localScale.x, yScale, LightEffect.transform.localScale.z);
            yield return null;
        }

        yield return null;
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

