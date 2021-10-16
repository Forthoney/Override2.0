using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : ShipWeapon
{
    public BulletWeapon(
        string PrefabName, 
        Vector2 PosOffsetAmt)
    {
        Damage = 10;
        Firerate = 2;
        BulletPrefab = PrefabName;
        SpritePosOffset = PosOffsetAmt;
    }
}
