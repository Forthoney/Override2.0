using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

  // Set by the ship weapon after spawn
  public float speed
  {
    get; set;
  }

  public float damage
  {
    get; set;
  }

  public GameObject OnHitEffect {get; set;}

  public bool isEnemyBullet;

  // Update is called once per frame
  void Update()
  {
    transform.position += transform.right * speed * Time.deltaTime;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (isEnemyBullet)
    {
      if (other.gameObject == GameManager.PlayerShip)
      {
        other.gameObject.GetComponent<ShipControlComponent>()?.takeDamage(damage);

        this.Die();
      }
    }
    else
    {
      if (other.gameObject != GameManager.PlayerShip)
      {
        other.gameObject.GetComponent<ShipControlComponent>()?.takeDamage(damage);
        ShockManager.Instance.StartShake(new Vector3(0, -2, 0));
        this.Die();
      }
    }
  }


  // TODO: add animation
  public void playDieAnimation()
  {

  }

  public void Die()
  {
	if (OnHitEffect != null)
		ParticleUtils.EmitOnce(OnHitEffect, transform);
    Destroy(gameObject);
  }

}
