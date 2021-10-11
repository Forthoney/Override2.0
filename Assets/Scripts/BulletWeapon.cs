using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : ShipWeapon
{
    public BulletWeapon(string PrefabName, Vector2 PosOffsetAmt)
    {
        this.damage = 5;
        this.FireRate = 3;
        this.BulletPrefab = PrefabName;
        this.SpritePosOffset = PosOffsetAmt;
    }

    public override void Fire(){
        // TODO: Load bulelt prefab from resource
        // TODO: instantiate bullet and set its owner
    }


}