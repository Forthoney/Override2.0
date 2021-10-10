using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : ShipWeapon
{
    public BulletWeapon(string PrefabName, Vector2 PosOffsetAmt)
    {
        damage = 5;
        Firerate = 3;
        BulletPrefab = PrefabName;
        SpritePosOffset = PosOffsetAmt;
    }


}