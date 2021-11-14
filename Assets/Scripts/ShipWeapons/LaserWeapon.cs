using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletWeapon", menuName = "~/Combat/BulletWeapon", order = 0)]
public class LaserWeapon : ShipWeapon
{
  public override void Fire(bool isEnemyBullet)
  {
    // Load and instantiate bullet prefab from resource
    GameObject laser = Instantiate(this._bulletPrefab, _firingSource.transform.position, _firingSource.transform.rotation);

    // Instantiate bullet fields
    laser.GetComponent<BulletBehaviour>().isEnemyBullet = isEnemyBullet;
    laser.GetComponent<BulletBehaviour>().damage = _damage;
    laser.GetComponent<BulletBehaviour>().speed = _bulletSpeed;

    // If this is a player bullet
    if (!isEnemyBullet)
    {
      laser.GetComponent<BulletBehaviour>().speed *= 2;
      ShockManager.Instance.StartShake(new Vector3(0, -0.5f, 0));
      laser.GetComponent<SpriteRenderer>().color = Color.red;
      TrailRenderer trail = laser.GetComponentInChildren<TrailRenderer>();
      Gradient gradient = new Gradient();
      gradient.SetKeys(
          new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
          new GradientAlphaKey[] { new GradientAlphaKey(1, 0.0f), new GradientAlphaKey(1, 1.0f) }
      );
      trail.colorGradient = gradient;
    }
  }
}
