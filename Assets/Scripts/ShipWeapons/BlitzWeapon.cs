using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlitzWeapon", menuName = "~/Combat/Weapons/BlitzWeapon", order = 2)]

public class BlitzWeapon : ShipWeapon
{
  // The number of bullets in each Shotgun shot
  [SerializeField] protected float numBulletsInShot = 7;
  [SerializeField] protected float bulletDelay = 0.1f;


  public override void Fire(bool isEnemyBullet)
  {
    base.Fire(isEnemyBullet);
    List<GameObject> bullets = new List<GameObject>();

    IEnumerator burst = FireBurst();
    GameManager.Instance.StartCoroutine(burst);

    IEnumerator FireBurst()
    {
      // rate of fire in weapons is in rounds per minute (RPM), therefore we should calculate how much time passes before firing a new round in the same burst.
      for (int i = 0; i < numBulletsInShot; i++)
      {
        GameObject bullet = Instantiate(this._bulletPrefab, _firingSource.transform.position, _firingSource.transform.rotation);
        bullet.GetComponent<BulletBehaviour>().SetProperties(isEnemyBullet, _damage, ShootEffectHitPrefab, _bulletSpeed);

        // Instantiate bullet fields
        _firingEffect?.GetComponent<ParticleCombo>()?.Play();

        // If this is a player bullet
        if (!isEnemyBullet)
        {
          bullet.GetComponent<BulletBehaviour>().Speed *= 2;
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

        yield return new WaitForSeconds(bulletDelay); // wait till the next round
      }
    }
  }



}
