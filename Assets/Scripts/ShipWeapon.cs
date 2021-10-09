using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipWeapon 
{
    public double Damage;

    public double Firerate;

    // called on trigger (click for player)
    public abstract void Fire();

    public string BulletPrefab;

    public Vector2 SpritePosOffset;
}
