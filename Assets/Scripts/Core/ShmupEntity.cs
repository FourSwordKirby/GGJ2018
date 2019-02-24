using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All Entities have health and can be hit by bullets. Some bullets can also be hit by other bullets
public abstract class ShmupEntity : MonoBehaviour {
    public abstract void OnHit(float damage);
    public abstract void OnStun();

    /// <summary>
    /// This refers to the general notion that an object is in a "compelted state". 
    /// This means an enemy has been defeated, barricade destoryed, door opened etc.
    /// </summary>
    public abstract bool IsCompleted();

    public abstract void Suspend();
    public abstract void Unsuspend();
}
