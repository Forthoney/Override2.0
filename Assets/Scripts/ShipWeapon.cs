using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipWeapon 
{
    protected double damage;

    // firerate is bullets per second
    public double FireRate;

    protected string bulletPrefab;

    protected Vector2 spritePosOffset;

    public double Damage
    {
        get;
        set;
    }

    public bool IsFiring
    {
        get;
        set;
    }

    public string BulletPrefab
    {
        get;
        set;
    }

    public Vector2 SpritePosOffset
    {
        get;
        set;
    }

    // called on trigger (click for player)
    public abstract void Fire();
}
