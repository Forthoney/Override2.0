using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour
{
    public ShipControlComponent enemyShip;

    protected float speed;
    protected float attackSpeed;

    private float speedMod = 0.5f;
    private float attackSpeedMod = 0.1f;

    private float fireTimer = 0;

    public EnemyBehaviour(ShipControlComponent enemyShip){
      this.enemyShip = enemyShip;

        speed = enemyShip.ShipBody.Speed * speedMod;
        attackSpeed = enemyShip.ShipWeapon.FireRate * attackSpeedMod;
    }

    public abstract void doAction();

    protected void rotateTowardsPlayer()
    {
        Vector3 thisToPlayer = GameManager.PlayerShip.transform.position - enemyShip.gameObject.transform.position;
        float angle = Mathf.Atan2(thisToPlayer.y, thisToPlayer.x) * Mathf.Rad2Deg;
        enemyShip.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    protected void fireWeapon()
    {
        if (fireTimer >= 1 / attackSpeed)
        {
            enemyShip.ShipWeapon.Fire(true);
            fireTimer = 0;
        }
        else
        {
            fireTimer += Time.deltaTime;
        }
    }
}
