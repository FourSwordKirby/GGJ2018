using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionEvent : ShmupGameEvent
{
    public string instruction;
    bool instructionOver = false;

    public override void RunEvent()
    {
        instructionOver = false;
        throw new Exception("Not Yet Implemented");
    }

    public override void EndEvent()
    {
        instructionOver = true;
    }

    public override bool EventCompleted()
    {
        return instructionOver;
    }
}
