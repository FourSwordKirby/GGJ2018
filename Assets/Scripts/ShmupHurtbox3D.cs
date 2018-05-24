using UnityEngine;
using System.Collections;

/*
 * The hurtbox does not respond when it's hit by something, it only provides ways for the hitbox to do different things to the hitboxes
 * The reason for this is because Hurtboxes have more consistent and adaptable behavior compared to other hitboxes
 */
public class ShmupHurtbox3D : Hurtbox3D
{
    public bool isPlayerOwned;
    void OnTriggerEnter(Collider col)
    {
        ShmupHitbox3D hitbox = col.GetComponent<ShmupHitbox3D>();
        if(hitbox != null && this.isPlayerOwned != hitbox.isPlayerOwned)
        {
            ShmupEntity entity = owner.GetComponent<ShmupEntity>();
            entity.OnHit(hitbox.damage);

            if (!hitbox.persistent)
                Destroy(hitbox.gameObject);

            //Visual hit effect stuff?
        }

        BombHitbox3D bombHitbox = col.GetComponent<BombHitbox3D>();
        if (bombHitbox != null && this.isPlayerOwned != bombHitbox.isPlayerOwned)
        {
            ShmupEntity entity = owner.GetComponent<ShmupEntity>();
            entity.OnStun();
        }
    }
}
