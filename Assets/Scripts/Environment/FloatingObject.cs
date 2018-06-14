using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour {

    public Rigidbody body;

    private void Start()
    {
        float v1 = Random.Range(0, 20f);
        float v2 = Random.Range(0, 20f);
        float v3 = Random.Range(0, 20f);
        body.angularVelocity = new Vector3(v1, v2, v3);   
    }
}
