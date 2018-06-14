using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterSpawner : MonoBehaviour {
    public List<Encounter> encounters;
    int waveNumber = 0;
    bool started;

    public void Awake()
    {
        foreach(Encounter e in encounters)
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
            encounters[waveNumber].StartEncounter();
        }
    }

    private void Update()
    {
        if (started && encounters[waveNumber].completed)
        {
            waveNumber++;
            if (waveNumber < encounters.Count)
                encounters[waveNumber].StartEncounter();
            else
                started = false;
        }
    }
}
