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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward);
        DamagedShip(hit.collider);
    }
}
