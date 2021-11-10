using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletWeapon", menuName = "~/Combat/BulletWeapon", order = 0)]
public class BulletWeapon : ShipWeapon
{
  public BulletWeapon(GameObject bPrefab, Vector2 sPosOffset, GameObject fSource) :
      base(1, 3, 20, fSource, bPrefab, sPosOffset, "Materials/Gun Small Enemy")
  {}

  public override void Fire(bool isEnemyBullet)
  {
    // Load and instantiate bullet prefab from resource
    GameObject bullet = Instantiate(this._bulletPrefab, _firingSource.transform.position, _firingSource.transform.rotation);

    // Instantiate bullet fields
    bullet.GetComponent<BulletBehaviour>().isEnemyBullet = isEnemyBullet;
    bullet.GetComponent<BulletBehaviour>().damage = _damage;
    bullet.GetComponent<BulletBehaviour>().speed = _bulletSpeed;


    // If this is a player bullet
    if (!isEnemyBullet)
    {
			bullet.GetComponent<BulletBehaviour>().speed *= 2;
			ShockManager.Instance.StartShake(new Vector3(0, -0.5f, 0));
			if (bullet.GetComponentInChildren<SpriteRenderer>() != null)
				bullet.GetComponentInChildren<SpriteRenderer>().color = Color.red;
			if (bullet.GetComponentInChildren<ParticleSystem>() != null) {
				ParticleSystem.MinMaxGradient col = bullet.GetComponentInChildren<ParticleSystem>().main.startColor;
				col.color = Color.red;
			}
		}
	}
}