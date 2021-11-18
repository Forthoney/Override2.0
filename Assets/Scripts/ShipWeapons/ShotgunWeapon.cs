using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunWeapon", menuName = "~/Combat/Weapons/ShotgunWeapon", order = 2)]

public class ShotgunWeapon : ShipWeapon
{
  // The number of bullets in each Shotgun shot
  [SerializeField] protected float numBulletsInShot;
  // The angle between each bullet
  [SerializeField] float spreadAngleInterval;
  [SerializeField] float startSpreadAngle;

  public override void Fire(bool isEnemyBullet)
  {
	base.Fire(isEnemyBullet);
    List<GameObject> bullets = new List<GameObject>();

    for (int i = 0; i < numBulletsInShot; i++)
    {
      GameObject bullet = Instantiate(this._bulletPrefab,
      _firingSource.transform.position,
      _firingSource.transform.rotation * Quaternion.Euler(0, 0, startSpreadAngle + i * spreadAngleInterval));
      // Instantiate bullet fields
      bullet.GetComponent<BulletBehaviour>().isEnemyBullet = isEnemyBullet;
      bullet.GetComponent<BulletBehaviour>().damage = _damage;
      bullet.GetComponent<BulletBehaviour>().speed = _bulletSpeed;
      bullet.GetComponent<BulletBehaviour>().OnHitEffect = ShootEffectHitPrefab;

      _firingEffect?.GetComponent<ParticleCombo>()?.Play();

      // If this is a player bullet
      if (!isEnemyBullet)
      {
        bullet.GetComponent<BulletBehaviour>().speed *= 2;
        ShockManager.Instance.StartShake(FiringSource.transform.rotation * Quaternion.Euler(0, 0, -90) * new Vector3(0, -1.5f, 0));
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



}
