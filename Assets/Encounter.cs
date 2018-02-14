using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour {

    public List<ShmupEnemy> enemies;

    public bool started;
    public bool completed;

	// Use this for initialization
	public void StartEncounter () {
        if (!started)
        {
            EnableEncounter();
            started = true;
            foreach (ShmupEnemy enemy in enemies)
            {
                enemy.Spawn();
            }
        }
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
        if (started && enemies.TrueForAll(x => x.gameObject.activeSelf == false))
            completed = true;
    }
}
