using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEvent : ShmupGameEvent
{
    public List<GameObject> ObjectsToSpawn;

    public override void EndEvent()
    {
        foreach (GameObject obj in ObjectsToSpawn)
        {
            ShmupSpawnable spawnable = obj.GetComponent<ShmupSpawnable>();
            if (spawnable == null)
                obj.SetActive(true);
            else
                spawnable.Spawn();
        }
    }

    public override bool EventCompleted()
    {
        return ObjectsToSpawn.TrueForAll(x => (x.GetComponent<ShmupSpawnable>() == null && x.activeSelf)
                                                || !x.GetComponent<ShmupSpawnable>().IsDead());
    }

    public override void RunEvent()
    {
        EndEvent();
    }
}
