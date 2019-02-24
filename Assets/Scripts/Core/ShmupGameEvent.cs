using UnityEngine;
using System.Collections;

public abstract class ShmupGameEvent : MonoBehaviour
{
    public abstract void RunEvent();
    /// <summary>
    /// Used to end game events early
    /// </summary>
    public abstract void EndEvent();
    public abstract bool EventCompleted();
}
