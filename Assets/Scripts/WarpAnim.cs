using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpAnim : MonoBehaviour {

    float destoryTime = 1.0f;
	// Update is called once per frame
	void Update () {
        destoryTime -= Time.deltaTime;
        if (destoryTime < 0)
            Destroy(this.gameObject);
	}
}
