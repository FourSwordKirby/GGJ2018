using UnityEngine;
using System.Collections;
using System;

public class Controls {

    public static Vector2 getDirection()
    {
        float xAxis = 0;
        float yAxis = 0;

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Keyboard Horizontal")))
            xAxis = Input.GetAxis("Horizontal");
        else
            xAxis = Input.GetAxis("Keyboard Horizontal");
        if (Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Abs(Input.GetAxis("Keyboard Vertical")))
            yAxis = Input.GetAxis("Vertical");
        else
            yAxis = Input.GetAxis("Keyboard Vertical");

        return new Vector2(xAxis, yAxis);
    }

    public static Parameters.InputDirection getInputDirection()
    {
        return Parameters.vectorToDirection(getDirection());
    }

    public static Vector2 getAimDirection()
    {
        float xAxis = 0;
        float yAxis = 0;

        if (Mathf.Abs(Input.GetAxis("AimX")) > Mathf.Abs(Input.GetAxis("Keyboard AimX")))
            xAxis = Input.GetAxis("AimX");
        else
            xAxis = Input.GetAxis("Keyboard AimX");

        if (Mathf.Abs(Input.GetAxis("AimY")) > Mathf.Abs(Input.GetAxis("Keyboard AimY")))
            yAxis = Input.GetAxis("AimY");
        else
            yAxis = Input.GetAxis("Keyboard AimY");

        Debug.Log(yAxis);

        return new Vector2(xAxis, yAxis);
    }

    internal static bool shootInputHeld()
    {
        return Input.GetButton("Shoot");
    }

    public static bool DirectionDown(Parameters.InputDirection dir)
    {
        //Hacky, probably should fix to be correct later
        Parameters.InputDirection currentInput = getInputDirection();
        return currentInput == dir;
    }

    internal static bool bombInputDown()
    {
        return Input.GetButton("Bomb");
    }

    public static bool confirmInputDown()
    {
        return Input.GetButtonDown("Confirm");
    }

    public static bool cancelInputDown()
    {
        return Input.GetButtonDown("Cancel");
    }

    public static bool confirmInputHeld()
    {
        return Input.GetButton("Confirm");
    }

    public static bool cancelInputHeld()
    {
        return Input.GetButton("Cancel");
    }

    public static bool pauseInputDown()
    {
        return Input.GetButtonDown("Pause");
    }

    public static bool pauseInputHeld()
    {
        return Input.GetButton("Pause");
    }
}
