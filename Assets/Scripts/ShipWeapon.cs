using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipWeapon 
{
    protected float damage;

    // firerate is bullets per second
    public float FireRate;

    protected string bulletPrefab;

    protected Vector2 spritePosOffset;

    public float Damage
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
        get => bulletPrefab;
        set => bulletPrefab = value;
    }

    public Vector2 SpritePosOffset
    {
        get;
        set;
    }

    // called on trigger (click for player)
    public abstract void Fire(bool isEnemyBullet);
}
