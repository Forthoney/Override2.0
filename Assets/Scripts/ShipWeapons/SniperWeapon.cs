using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SniperWeapon", menuName = "~/Combat/Weapons/SniperWeapon", order = 4)]
public class SniperWeapon : ShipWeapon
{
    public override void Fire(bool isEnemyBullet)
    {
        base.Fire(isEnemyBullet);
        RaycastHit2D hit = Physics2D.Raycast(_firingSource.transform.position,
            _firingSource.transform.TransformDirection(Vector2.up));

        // If this is a player bullet
        if (!isEnemyBullet)
        {
            
        }
    }
}