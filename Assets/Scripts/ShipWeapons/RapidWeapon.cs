using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RapidWeapon", menuName = "~/Combat/Weapons/RapidWeapon", order = 4)]
public class RapidWeapon : ShipWeapon
{
  public override void Fire(bool isEnemyBullet)
  {
	  base.Fire(isEnemyBullet);

    // Load and instantiate bullet prefab from resource
    GameObject bullet = Instantiate(this._bulletPrefab, _firingSource.transform.position, _firingSource.transform.rotation);

    // Instantiate bullet fields
    bullet.GetComponent<BulletBehaviour>().SetProperties(isEnemyBullet, _damage, ShootEffectHitPrefab, _bulletSpeed);
    
    _firingEffect?.GetComponent<ParticleCombo>()?.Play();

    // If this is a player bullet
    if (!isEnemyBullet)
    {
      bullet.GetComponent<BulletBehaviour>().Speed *= 2;
      ShockManager.Instance.StartShake(new Vector3(0, -0.5f, 0));
      if (bullet.GetComponentInChildren<SpriteRenderer>() != null)
        bullet.GetComponentInChildren<SpriteRenderer>().color = Color.red;

      TrailRenderer trail = bullet.GetComponentInChildren<TrailRenderer>();
      Gradient gradient = new Gradient();
      gradient.SetKeys(
          new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
          new GradientAlphaKey[] { new GradientAlphaKey(0.5f, 0.0f), new GradientAlphaKey(0, 1.0f) }
      );
      trail.colorGradient = gradient;
    }
  }
}
