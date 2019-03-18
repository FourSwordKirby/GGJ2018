using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedText : MonoBehaviour {
    public float duration;
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;
		if(duration < 0)
        {
            this.gameObject.SetActive(false);
        }
	}
}
