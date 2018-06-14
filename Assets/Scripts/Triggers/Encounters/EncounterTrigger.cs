using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTrigger : MonoBehaviour {

    public EncounterSpawner linkedEncounter;

    private void OnTriggerEnter(Collider col)
    {
        Hurtbox3D colBox = col.GetComponent<Hurtbox3D>();
        if (colBox != null && colBox.owner.GetComponent<ShmupPlayer>() != null)
        {
            linkedEncounter.StartEncounter();
            Destroy(this.gameObject);
        }
    }
}
