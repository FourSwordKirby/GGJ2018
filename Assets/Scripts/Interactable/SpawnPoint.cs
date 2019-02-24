using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : ShmupEntity, ShmupSpawnable {

    public int hackingThreshold;

    private int hackingProgress;
    public float decayRate; //We lose 1 hacking progress point every x seconds
    public bool activated;

    public Renderer selfRenderer;
    public Light spotLight;
    public GameObject particles;

    public Material onMaterial;
    public Material offMaterial;

    float timer;
	// Update is called once per frame
	void Update () {
        //Controls decaying hacking rate
        if(hackingProgress > 0)
        {
            timer += Time.deltaTime;
            if(timer > decayRate)
            {
                timer = 0;
                hackingProgress--;
            }
        }

        //Visual activated indicator
        if (activated)
        {
            selfRenderer.material = onMaterial;
            spotLight.enabled = true;
            particles.SetActive(true);
        }
        else
        {
            spotLight.enabled = false;
            selfRenderer.material = offMaterial;
            particles.SetActive(false);

        }

        //Animations

    }

    public override void OnHit(float damage)
    {
        hackingProgress++;

        if (hackingProgress > hackingThreshold)
        {
            hackingProgress = 0;
            activated = !activated;
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
        return;
    }

    public override void Unsuspend()
    {
        return;
    }

    public void Spawn()
    {
        activated = true;
    }

    public void Die()
    {
        activated = false;
    }

    public bool IsDead()
    {
        return !activated;
    }
}
