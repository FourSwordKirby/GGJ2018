using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickSpaceInvaderMovement : ShmupEntity {

    public float HorizontalInterval;
    public float VerticalInterval;

    float horizontalTimer;
    float verticalTimer;

    float vert_Limit = 0;

    bool moveLeft;

    public override bool IsCompleted()
    {
        return true;
    }

    public override void OnHit(float damage)
    {
    }

    public override void OnStun()
    {
    }

    public override void Suspend()
    {
        this.enabled = false;
    }

    public override void Unsuspend()
    {
        this.enabled = true;
    }

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
            if(this.transform.position.z - 2 > vert_Limit)
                this.transform.position -= Vector3.forward * 2;
            verticalTimer = 0;
        }
	}
}
