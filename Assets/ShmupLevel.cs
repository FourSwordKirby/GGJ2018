using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ShmupLevel: MonoBehaviour {
    public GameObject LevelObject;

    public abstract void StartLevel();
    public abstract void EndLevel();
    public abstract bool IsFinished();

    public List<SpawnPoint> GetSpawnPoints()
    {
        return LevelObject.GetComponentsInChildren<SpawnPoint>(true).ToList();
    }
}
