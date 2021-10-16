using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : ShipWeapon
{
    GameObject _firingSource;

    public LaserWeapon(string PrefabName, Vector2 PosOffsetAmt, GameObject FiringSource)
    {
        this.damage = 10;
        this.FireRate = 2;
        this.BulletPrefab = PrefabName;
        this.SpritePosOffset = PosOffsetAmt;
		this._firingSource = FiringSource;
    }

    public override void Fire(){
        // TODO: Load laser prefab from resource
        // TODO: instantiate laser and set its owner
		GameObject laser = Object.Instantiate<GameObject>(Resources.Load<GameObject>(this.bulletPrefab), 
			_firingSource.transform.position, _firingSource.transform.rotation);
    }

}
