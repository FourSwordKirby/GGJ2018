using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keygen : MonoBehaviour
{
    public int hackingThreshold;

    private int hackingProgress;
    public float decayRate; //We lose 1 hacking progress point every x seconds

    public Renderer selfRenderer;

    float timer;

    public GameObject currentOwner;

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
    }

    public void OnHit(float damage, GameObject owner)
    {
        if (owner != currentOwner)
        {
            currentOwner = owner;
            ResetProgress();
        }

        hackingProgress++;

        if (hackingProgress > hackingThreshold)
            GrantKey();
    }

    void ResetProgress()
    {
        hackingProgress = 0;
    }

    void GrantKey()
    {
        if(currentOwner.GetComponent<ShmupPlayer>() != null)
        {
            currentOwner.GetComponent<ShmupPlayer>().GainBomb(ShmupPlayer.MIN_BOMB_CHARGE);
            hackingProgress = 0;
        }
    }
}