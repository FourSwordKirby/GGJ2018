using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickSpaceInvaderMovement : MonoBehaviour {

    public float HorizontalInterval;
    public float VerticalInterval;

    float horizontalTimer;
    float verticalTimer;

    bool moveLeft;

	// Update is called once per frame
	void Update () {
        horizontalTimer += Time.deltaTime;
        verticalTimer += Time.deltaTime;
        if(horizontalTimer > HorizontalInterval)
        {
            if (moveLeft)
                this.transform.position += Vector3.left * 2;
            else
                this.transform.position += Vector3.right * 2;
            horizontalTimer = 0;
        }

        if(verticalTimer > VerticalInterval)
        {
            moveLeft = !moveLeft;
            this.transform.position += Vector3.back * 2;
            verticalTimer = 0;
        }
	}
}
