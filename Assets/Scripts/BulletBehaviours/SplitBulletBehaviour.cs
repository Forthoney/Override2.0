using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBulletBehaviour : BulletBehaviour
{
  public int numChildren;

  public GameObject childBullet;
  public float startSpreadAngle, spreadAngleInterval;

  void Update()
  {
    Debug.Log("Bullet moving");
    transform.position += transform.right * Speed * Time.deltaTime;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (DamagedShip(other)) // Checking if the collision is a valid hit 
    {
      for (int i = 0; i < numChildren; i++)
      {
        GameObject bullet = Instantiate(childBullet, transform.position + Vector3.forward * 20,
        transform.rotation * Quaternion.Euler(0, 0, startSpreadAngle - i * spreadAngleInterval));
        bullet.GetComponent<BulletBehaviour>().SetProperties(isFromEnemy, Damage, OnHitEffect, Speed);

        if (!isFromEnemy)
        {
          bullet.GetComponent<BulletBehaviour>().Speed *= 2;
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
      this.Die();
    }
  }

  // Called when the bullet dies
  private void Die()
  {
    if (OnHitEffect != null)
      ParticleUtils.EmitOnce(OnHitEffect, transform);
    Destroy(gameObject);
  }

}
