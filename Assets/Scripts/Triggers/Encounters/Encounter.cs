using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour {
    public List<Wave> waves;
    int waveNumber = 0;
    bool started;

    public void Awake()
    {
        foreach(Wave e in waves)
        {
            e.DisableEncounter();
        }
    }

    public void StartEncounter()
    {
        if (!started)
        {
            started = true;
            waveNumber = 0;
            waves[waveNumber].StartWave();
        }
    }

    private void Update()
    {
        if (!IsCompleted() && started && waves[waveNumber].IsCompleted())
        {
            waveNumber++;
            if (waveNumber < waves.Count)
                waves[waveNumber].StartWave();
            else
                started = false;
        }
    }

    public bool IsCompleted()
    {
        return waveNumber >= waves.Count;
    }
}
