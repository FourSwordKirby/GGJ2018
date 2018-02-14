using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{

    public bool withPortraits;
    public bool isCutscene;
    public TextAsset dialog;

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        Hurtbox3D hurtbox = col.gameObject.GetComponent<Hurtbox3D>();
        if (hurtbox != null)
        {
            Debug.Log("triggered");
            if (hurtbox.owner.GetComponent<ShmupPlayer>() != null)
            {
                if (isCutscene)
                    StartCoroutine(ShmupGameManager.instance.PlayCutscene(dialog, withPortraits));
                else
                    StartCoroutine(ShmupGameManager.instance.StartConversation(dialog, withPortraits));
                this.gameObject.SetActive(false);
            }
        }
    }
}