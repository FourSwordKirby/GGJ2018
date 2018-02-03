using UnityEngine;
using System.Collections;

/*
 * The hurtbox does not respond when it's hit by something, it only provides ways for the hitbox to do different things to the hitboxes
 * The reason for this is because Hurtboxes have more consistent and adaptable behavior compared to other hitboxes
 */
public class KeygenHurtbox : Hurtbox3D
{
    void OnTriggerEnter(Collider col)
    {
        Hitbox3D hitbox = col.GetComponent<Hitbox3D>();
        if(hitbox != null && hitbox.owner != this.owner)
        {
            Keygen entity = owner.GetComponent<Keygen>();
            
            entity.OnHit(hitbox.damage, hitbox.owner);

            if (!hitbox.persistent)
                Destroy(hitbox.gameObject);

            //Visual hit effect stuff?
            //Vector3 hitLocation = (this.transform.position + col.bounds.ClosestPoint(this.transform.position)) / 2.0f;
        }
    }
}
