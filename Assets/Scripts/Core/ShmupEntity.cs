using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All Entities have health and can be hit by bullets. Some bullets can also be hit by other bullets
public abstract class ShmupEntity : MonoBehaviour {
    public abstract void OnHit(float damage);
    public abstract void OnStun();
}
