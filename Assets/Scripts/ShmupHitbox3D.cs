﻿using UnityEngine;
using System.Collections;

/*
 * The hurtbox does not respond when it's hit by something, it only provides ways for the hitbox to do different things to the hitboxes
 * The reason for this is because Hurtboxes have more consistent and adaptable behavior compared to other hitboxes
 */
public class ShmupHitbox3D : Hitbox3D
{
    public bool isPlayerOwned;
    public bool persistent;
}
