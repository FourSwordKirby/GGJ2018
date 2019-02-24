using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShmupMasterNode : ShmupSimpleEnemy {

    public List<GameObject> linkedGameObjects = new List<GameObject>();

    private List<ShmupEntity> linkedEntities = new List<ShmupEntity>();
    private List<ShmupSpawnable> linkedSpawnableEntities = new List<ShmupSpawnable>();

    public Material lineRendererMaterial;
    private LineRenderer lineRenderer;
    new void Awake()
    {
        base.Awake();

        linkedEntities = linkedGameObjects.Select(x => x.GetComponentsInChildren<ShmupEntity>().ToList()).SelectMany(x => x).ToList();
        linkedSpawnableEntities = linkedGameObjects.Select(x => x.GetComponentsInChildren<ShmupSpawnable>().ToList()).SelectMany(x => x).ToList();

        lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineRendererMaterial;
        lineRenderer.widthMultiplier = 0.2f;

        lineRenderer.positionCount = (linkedEntities.Count * 2) + 1;
    }

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
        List<ShmupEntity> aliveEntities = linkedEntities.Where(x => !x.IsCompleted()).ToList();
        lineRenderer.SetPosition(0, this.transform.position);

        for (int i = 0; i < aliveEntities.Count(); i++)
        {
            ShmupEntity entity = aliveEntities[i];

            lineRenderer.SetPosition(2*i+1, this.transform.position);
            lineRenderer.SetPosition(2*i+2, entity.gameObject.transform.position);
        }

        for (int i = aliveEntities.Count; i < linkedEntities.Count(); i++)
        {
            lineRenderer.SetPosition(2 * i + 1, this.transform.position);
            lineRenderer.SetPosition(2 * i + 2, this.transform.position);
        }
    }

    public override void OnHit(float damage)
    {
        PingEffect();
        base.OnHit(damage);
    }

    public override void Spawn()
    {
        base.Spawn();
        lineRenderer.enabled = true;
    }

    public override void Die()
    {
        foreach(ShmupSpawnable entity in linkedSpawnableEntities)
        {
            if(!entity.IsDead())
                entity.Die();
        }
        lineRenderer.enabled = false;
        base.Die();
    }

    private void PingEffect()
    {
        GameObject ping = Instantiate(pingEffect);
        ping.transform.position = this.transform.position + Vector3.up * 0.1f;
        ping.GetComponent<PingEffect>().duration = 0.5f;
    }
}
