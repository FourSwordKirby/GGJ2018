using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingLight : MonoBehaviour {

    Light spotLight;

	// Use this for initialization
	void Start () {
        spotLight = this.GetComponent<Light>();
	}

    float pulseRate = 0.025f;
	// Update is called once per frame
	void Update ()
    {
        spotLight.intensity += pulseRate;
        if (!(1.0f < spotLight.intensity && spotLight.intensity < 2.0f))
            pulseRate *= -1;
    }
}
