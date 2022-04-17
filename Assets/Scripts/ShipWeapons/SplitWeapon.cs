using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SplitWeapon", menuName = "~/Combat/Weapons/SplitWeapon", order = 5)]
public class SplitWeapon : ShipWeapon
{
  public override void Fire(bool isFromEnemy)
  {
	base.Fire(isFromEnemy);
    // Load and instantiate bullet prefab from resource
    GameObject bullet = Instantiate(this._bulletPrefab, _firingSource.transform.position, _firingSource.transform.rotation);

    // Instantiate bullet fields
    bullet.GetComponent<BulletBehaviour>().isFromEnemy = isFromEnemy;
    bullet.GetComponent<BulletBehaviour>().Damage = _damage;
    bullet.GetComponent<BulletBehaviour>().Speed = _bulletSpeed;
    bullet.GetComponent<BulletBehaviour>().OnHitEffect = ShootEffectHitPrefab;

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
