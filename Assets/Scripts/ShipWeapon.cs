using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipWeapon 
{
    public double Damage;

    public double Firerate;

    public string BulletPrefab;

    public Vector2 SpritePosOffset;

    // called on trigger (click for player)
    public abstract void Fire();
}
