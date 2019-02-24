using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public List<GameObject> spawnables;
    public List<GameObject> keyGameObjects;
    public bool started;
    public bool completed;

    public List<ShmupGameEvent> events;
    public WaveTrigger nextWaveTrigger;

    private List<ShmupEnemy> enemies;
    private List<ShmupSpawnable> objectsToSpawn;
    private List<ShmupEntity> objectsToShoot;

    public void Awake()
    {
        enemies = spawnables.Select(x => x.GetComponentsInChildren<ShmupEnemy>().ToList()).SelectMany(x => x).ToList();
        objectsToSpawn = spawnables.Select(x => x.GetComponentsInChildren<ShmupSpawnable>().ToList()).SelectMany(x => x).ToList();
        objectsToShoot = keyGameObjects.Select(x => x.GetComponentsInChildren<ShmupEntity>().ToList()).SelectMany(x => x).ToList();

        //spawnables.Where(x => x is ShmupSpawnable).Select(x => (ShmupSpawnable)x).ToList();
    }

    // Use this for initialization
    public void StartWave () {
        if (!started)
        {
            EnableEncounter();
            started = true;
            foreach (ShmupSpawnable spawn in objectsToSpawn)
                spawn.Spawn();
        }
        RunGameEvents();
    }


    public void FinishWave()
    {
        foreach (ShmupSpawnable spawn in objectsToSpawn)
        {
            if(!spawn.IsDead())
                spawn.Die();
        }
        completed = true;
    }

    public bool IsCompleted()
    {
        return completed;
    }

    public void EnableEncounter()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableEncounter()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if ((started && objectsToShoot.TrueForAll(x => x.IsCompleted())))    // || Controls.dialogAdvanceDown())
        {
            if (nextWaveTrigger == null)
                FinishWave();
            else
                nextWaveTrigger.Enable();
        }
    }

    private void RunGameEvents()
    {
        foreach (ShmupGameEvent gameEvent in events)
        {
            GameEventQueue.instance.AddGameEvent(gameEvent);
        }
    }

}
