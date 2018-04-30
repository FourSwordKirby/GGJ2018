using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour {

    public Transform playerOrientation;
    public Rigidbody selfBody;

    public GameObject LeftWingMain;
    public GameObject RightWingMain;

    public GameObject LeftWingFeather1;
    public GameObject LeftWingFeather2;
    public GameObject LeftWingFeather3;

    public GameObject RightWingFeather1;
    public GameObject RightWingFeather2;
    public GameObject RightWingFeather3;

    public GameObject SubLeftWingFeather1;
    public GameObject SubLeftWingFeather2;
    public GameObject SubLeftWingFeather3;
    public GameObject SubRightWingFeather1;
    public GameObject SubRightWingFeather2;
    public GameObject SubRightWingFeather3;

    // Update is called once per frame
    void Update () {
        Vector3 currentVelocity = selfBody.velocity.normalized;

        Vector3 xproj = Vector3.Project(currentVelocity, transform.right.normalized);
        Vector3 zproj = Vector3.Project(currentVelocity, transform.forward.normalized);

        float x = xproj.magnitude * Mathf.Sign(Vector3.Dot(xproj, transform.right.normalized));
        float z = zproj.magnitude * Mathf.Sign(Vector3.Dot(zproj, transform.forward.normalized));


        float maxDrift = 25;
        float maxSpread = 15;

        turnTowards(SubLeftWingFeather1, -maxSpread * z + maxDrift * x);
        turnTowards(SubLeftWingFeather2, 0 + maxDrift * x);
        turnTowards(SubLeftWingFeather3, maxSpread * z + maxDrift * x);
        turnTowards(SubRightWingFeather1, -maxSpread * z + maxDrift * x);
        turnTowards(SubRightWingFeather2, 0 + maxDrift * x);
        turnTowards(SubRightWingFeather3, maxSpread * z + maxDrift * x);
    }

    void turnTowards(GameObject obj, float drift)
    {
        Vector3 currentRotation = obj.transform.localRotation.eulerAngles;

        //float targetRotation = 0;
        //if (Mathf.Abs(currentRotation) > 180)
        //    targetRotation = -360.0f;
        //else
        //    targetRotation = 0.0f;


        float distance = currentRotation.y * 0.3f;
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.localRotation, Quaternion.Euler(Vector3.up * drift), Time.deltaTime * distance);
        //obj.transform.localRotation = Quaternion.Euler(Vector3.zero); ;
        //obj.transform.localRotation = Quaternion.Euler(Vector3.up * Mathf.MoveTowards(currentRotation, targetRotation, turnSpeed));
    }
}
