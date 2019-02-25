using UnityEngine;
using System.Collections;

/*
 * The hurtbox does not respond when it's hit by something, it only provides ways for the hitbox to do different things to the hitboxes
 * The reason for this is because Hurtboxes have more consistent and adaptable behavior compared to other hitboxes
 */
public class WirelessPortHurtbox : Hurtbox3D
{
    void OnTriggerEnter(Collider col)
    {
        Hurtbox3D colBox = col.GetComponent<Hurtbox3D>();
        if (colBox != null && colBox.owner.GetComponent<ShmupPlayer>() != null)
        {
            WirelessPort entity = owner.GetComponent<WirelessPort>();
            entity.OnHit(0, colBox.owner);
        }
    }
}
