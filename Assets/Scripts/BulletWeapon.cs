using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : ShipWeapon
{
	GameObject _firingSource;

    public BulletWeapon(string PrefabName, Vector2 PosOffsetAmt, GameObject FiringSource)
    {
        this.damage = 5;
        this.FireRate = 3;
        this.BulletPrefab = PrefabName;
        this.SpritePosOffset = PosOffsetAmt;
		this._firingSource = FiringSource;
    }

    public override void Fire(){
        // TODO: Load bulelt prefab from resource
        // TODO: instantiate bullet and set its owner
		GameObject bullet = Object.Instantiate<GameObject>(Resources.Load<GameObject>(this.bulletPrefab), 
			_firingSource.transform.position, _firingSource.transform.rotation);
    }


}