using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBit : MonoBehaviour {

    public MeshRenderer selfRenderer;
    public Rigidbody selfbody;
    public float trackingSpeed;

    private GameObject target;

	// Use this for initialization
	void Awake () {
        selfbody = this.GetComponent<Rigidbody>();
        selfRenderer = this.GetComponent<MeshRenderer>();
	}
	
    public void TrackObject(GameObject obj)
    {
        target = obj;
    }

    public void SetColor(Color color)
    {
        selfRenderer.material.color = color;//.SetColor("_Color", color);
    }

    void Update()
    {
        if(target != null)
        {
            this.selfbody.velocity = (target.transform.position - this.transform.position) * trackingSpeed;
        }
    }
}
