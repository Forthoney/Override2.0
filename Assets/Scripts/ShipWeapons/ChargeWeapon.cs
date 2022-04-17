using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeWeapon", menuName = "~/Combat/Weapons/ChargeWeapon", order = 1)]
public class ChargeWeapon : ShipWeapon
{
  [SerializeField] private float _maxChargeTime;
  [SerializeField] private float _maxChargeDamageMod;
  [SerializeField] private float _maxChargeSpeedMod;

  private float _chargedTIme;

  public override void Fire(bool isFromEnemy)
  { 
    base.Fire(isFromEnemy);
    // Load and instantiate bullet prefab from resource
    GameObject laser = Instantiate(this._bulletPrefab, _firingSource.transform.position, _firingSource.transform.rotation);

    // Instantiate bullet fields
    laser.GetComponent<BulletBehaviour>().SetProperties(isFromEnemy, _damage, ShootEffectHitPrefab, _bulletSpeed);


    // If this is a player bullet
    if (!isFromEnemy)
    {
      laser.GetComponent<BulletBehaviour>().Speed *= 2;
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
