using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletWeapon", menuName = "~/Combat/Weapons/BulletWeapon", order = 0)]
public class BulletWeapon : ShipWeapon
{
  public override void Fire(bool isFromEnemy)
  {
	  base.Fire(isFromEnemy);
    // Load and instantiate bullet prefab from resource
    GameObject bullet = Instantiate(this._bulletPrefab, _firingSource.transform.position, _firingSource.transform.rotation);

    // Instantiate bullet fields
    bullet.GetComponent<BulletBehaviour>().SetProperties(isFromEnemy, _damage, ShootEffectHitPrefab, _bulletSpeed);
    
    _firingEffect?.GetComponent<ParticleCombo>()?.Play();

    // If this is a player bullet
    if (!isFromEnemy)
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