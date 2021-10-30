using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour
{
    public ShipControlComponent enemyShip;
    // Start is called before the first frame update

    public EnemyBehaviour(ShipControlComponent enemyShip){
      this.enemyShip = enemyShip;
    }

    public abstract void doAction();
}
