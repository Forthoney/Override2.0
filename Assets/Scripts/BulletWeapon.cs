using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : ShipWeapon
{
  public BulletWeapon(string bPrefab, Vector2 sPosOffset, GameObject fSource) :
      base(5, 3, 10, fSource, bPrefab, sPosOffset)
  { }

  public override void Fire(bool isEnemyBullet)
  {
    // Load and instantiate bullet prefab from resource
    GameObject bullet = Object.Instantiate<GameObject>(Resources.Load<GameObject>(this._bulletPrefab),
      _firingSource.transform.position, _firingSource.transform.rotation);
    // Instantiate bullet fields
    bullet.GetComponent<BulletBehaviour>().isEnemyBullet = isEnemyBullet;
    bullet.GetComponent<BulletBehaviour>().damage = _damage;
    bullet.GetComponent<BulletBehaviour>().speed = _bulletSpeed;
  }
}