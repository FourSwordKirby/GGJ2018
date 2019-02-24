using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : MonoBehaviour {
    public ShmupLevel associatedLevel;
    private void OnTriggerEnter(Collider col)
    {
        Hurtbox3D colBox = col.GetComponent<Hurtbox3D>();
        if (colBox != null && colBox.owner.GetComponent<ShmupPlayer>() != null)
        {
            associatedLevel.EndLevel();
        }
    }
}
