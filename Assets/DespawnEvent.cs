using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnEvent : ShmupGameEvent
{
    public List<GameObject> ObjectsToDespawn;

    public override void EndEvent()
    {
        foreach (GameObject obj in ObjectsToDespawn)
        {
            ShmupSpawnable spawnable = obj.GetComponent<ShmupSpawnable>();
            if (spawnable == null)
                obj.SetActive(false);
            else
                spawnable.Die();
        }
    }

    public override bool EventCompleted()
    {
        return ObjectsToDespawn.TrueForAll(x => (x.GetComponent<ShmupSpawnable>() == null && !x.activeSelf)
                                                || x.GetComponent<ShmupSpawnable>().IsDead());
    }

    public override void RunEvent()
    {
        EndEvent();
    }
}
