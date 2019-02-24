using System.Collections;
using System.Collections.Generic;
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

    public override void OnHit(float damage)
    {
        hackingProgress++;

        if (hackingProgress > hackingThreshold)
        {
            hackingProgress = 0;
            unlocked = !unlocked;
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