using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBit : MonoBehaviour {

    public MeshRenderer selfRenderer;
    public Rigidbody selfbody;
    public float trackingSpeed;

    private ShmupEntity target;

    const float DECAY_WARNING_TIME = 8.0f;
    const float MAX_PERSIST_TIME = 10.0f;

    /// <summary>
    /// This is the amount of bomb/special resource that is charged by collecting this bit
    /// </summary>
    public int chargeValue;

	// Use this for initialization
	void Awake () {
        selfbody = this.GetComponent<Rigidbody>();
        selfRenderer = this.GetComponent<MeshRenderer>();
	}
	
    public void TrackObject(ShmupEntity obj)
    {
        target = obj;
    }

    public void SetColor(Color color)
    {
        selfRenderer.material.color = color;//.SetColor("_Color", color);
    }

    float timer;
    void Update()
    {
        if(target != null)
        {
            Vector3 displacementVector = target.transform.position - transform.position;
            this.selfbody.velocity = (displacementVector).normalized * Mathf.Max(trackingSpeed, displacementVector.magnitude);

            if (Vector3.Distance(transform.position, target.transform.position) < 0.3f)
            {
                Destroy(this.gameObject);

                //Adds bomb
                ShmupPlayer player = target.GetComponent<ShmupPlayer>();
                if (player != null)
                {
                    player.GainBomb(this.chargeValue);
                }
            }
        }

        timer += Time.deltaTime;
        if(timer > DECAY_WARNING_TIME)
            SetColor(Color.grey);
        if (timer > MAX_PERSIST_TIME)
            Destroy(this.gameObject);
    }
}
