using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SecurityLock : ShmupEntity
{
    public int hackingThreshold;

    private int hackingProgress;
    public float decayRate; //We lose 1 hacking progress point every x seconds
    public bool unlocked;

    public Renderer selfRenderer;
    public Material onMaterial;
    public Material offMaterial;

    private List<GameObject> linkedObjects = new List<GameObject>();

    float timer;
    // Update is called once per frame
    void Update()
    {
        //Controls decaying hacking rate
        if (hackingProgress > 0)
        {
            timer += Time.deltaTime;
            if (timer > decayRate)
            {
                timer = 0;
                hackingProgress--;
            }
        }

        //Visual activated indicator
        if (unlocked)
            selfRenderer.material = onMaterial;
        else
            selfRenderer.material = offMaterial;
    }

    public void AddLinkedObject(GameObject obj)
    {
        linkedObjects.Add(obj);
    }

    public override void OnHit(float damage)
    {
        hackingProgress++;

        if (hackingProgress > hackingThreshold)
        {
            hackingProgress = 0;
            unlocked = true;
        }

        if(!unlocked)
        {
            foreach (Vector3 pos in linkedObjects.Select(x => x.transform.position))
            {
                EffectManager.instance.SpawnLinkSignal(transform.position, pos, true);
            }
        }
    }

    public override void OnStun()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsCompleted()
    {
        throw new System.NotImplementedException();
    }

    public override void Suspend()
    {
        throw new System.NotImplementedException();
    }

    public override void Unsuspend()
    {
        //throw new System.NotImplementedException();
    }
}