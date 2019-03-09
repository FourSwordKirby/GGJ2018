using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceAnimation : MonoBehaviour {

    public List<GameObject> EntranceAnimationEffects;

    float syncTimer = 1.0f;
	// Update is called once per frame
	void Update () {
        if (syncTimer > 0)
        {
            syncTimer -= Time.deltaTime;
            return;
        }
        for(int i = 0; i < EntranceAnimationEffects.Count; i++)
        {
            GameObject effect = EntranceAnimationEffects[i];

            if (effect.transform.localPosition.z > -1)
            {
                effect.transform.localPosition = Vector3.back * 5.0f;
                effect.transform.localScale = Vector3.one;
            }
            effect.transform.localPosition += Vector3.forward * 5.0f * Time.deltaTime;
            effect.transform.localScale -= Vector3.one * 0.5f * Time.deltaTime;
        }
	}
}
