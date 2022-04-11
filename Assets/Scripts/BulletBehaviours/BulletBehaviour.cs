using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : DamageBehavior
{
  // Set by the ship weapon after spawn
  public float Speed
  {
    get; set;
  }

  public bool pierces = false;

  public void SetProperties(bool fromEnemy, float damage, GameObject onHitEffect, float bulletSpeed)
  {
    base.SetProperties(fromEnemy, damage, onHitEffect);
    Speed = bulletSpeed;
    Debug.Log("Bullet made. Speed: " + bulletSpeed);
  }

  void Update()
  {
    Debug.Log("Bullet moving");
    transform.position += transform.right * Speed * Time.deltaTime;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (DamagedShip(other)) // Checking if the collision is a valid hit 
    {
      if (!pierces)
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
