using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipWeapon 
{
    private double damage;

    // firerate is per second (?)
    private bool isFiring;

    private string bulletPrefab;

    private Vector2 spritePosOffset;

    public double Damage
    {
        get { return damage; }
    }

    public double IsFiring
    {
        get { return isFiring; }
    }

    public double BulletPrefab
    {
        get { return bulletPrefab; }
    }

    public double SpritePosOffset
    {
        get { return spritePosOffset; }
    }

    // called on trigger (click for player)
    public abstract void Fire();
}
