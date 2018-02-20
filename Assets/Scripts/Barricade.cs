using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : ShmupEntity {

    public int hackingThreshold;
    public int hackingProgress;
    public bool destroyed;


    public GameObject model;
    
    // Update is called once per frame
    void Update()
    {
        model.SetActive(!destroyed);
    }

    public override void OnHit(float damage)
    {
        hackingProgress++;

        if (hackingProgress >= hackingThreshold)
        {
            hackingProgress = 0;
            destroyed = true;
        }
    }
}
