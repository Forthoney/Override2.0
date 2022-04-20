using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class HitscanBehaviour : DamageBehaviour
{
  public new void SetProperties(bool fromEnemy, float damage, GameObject onHitEffect)
  {
    base.SetProperties(fromEnemy, damage, onHitEffect);
  }

  public void Fire()
  {
    RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right * 1000);

    foreach (RaycastHit2D hit in hits)
    {
      DamagedShip(hit.collider);
    }
  }

  float timer = 0f;

  void Update()
  {
    if (timer > 2) Destroy(this);
    timer += Time.deltaTime;
  }
}
