using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterSpawner : MonoBehaviour {
    public List<Encounter> encounters;
    int waveNumber = 0;
    bool started;

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
        if (Input.GetKeyDown(KeyCode.A))
            StartEncounter();

        if(started && encounters[waveNumber].completed)
        {
            waveNumber++;
            if(waveNumber < encounters.Count)
                encounters[waveNumber].StartEncounter();
        }
    }
}
