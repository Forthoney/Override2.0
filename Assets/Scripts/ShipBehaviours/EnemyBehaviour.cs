using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour
{
    public ShipControlComponent enemyShip;

    protected float speed;
    protected float attackSpeed;

    private float speedMod = 0.33f; // FIXME: one day, move into separate thing which considers all buffs
    private float attackSpeedMod = 0.2f; // FIXME: one day, move into separate thing which considers all buffs

    private float fireTimer = 0;

    public EnemyBehaviour(ShipControlComponent enemyShip){
      this.enemyShip = enemyShip;

        speed = enemyShip.ShipBody.Speed * speedMod;
        attackSpeed = enemyShip.ShipWeapon.FireRate * attackSpeedMod;
    }

    public abstract void doAction();

    protected void rotateTowardsPlayer()
    {
      enemyShip.ShipBody.rotateTowardsWorldPos(enemyShip.gameObject, GameManager.PlayerShip.transform.position);
    }

    protected void fireWeapon()
    {
        if (fireTimer >= 1 / attackSpeed && !enemyShip.AwaitingFiring)
        {
            // enemyShip.ShipWeapon.Fire(true);
			// We now trigger the animation instead
			enemyShip.AwaitingFiring = true;
			enemyShip.GetComponent<Animator>()?.SetTrigger("Attack");
            fireTimer = 0;
        }
        else
        {
            fireTimer += Time.deltaTime;
        }
    }
}
