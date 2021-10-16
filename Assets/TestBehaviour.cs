using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShipControlComponent enemyShipControl = gameObject.GetComponent<ShipControlComponent>();
        BasicBehaviour enemyBehaviour = new BasicBehaviour(enemyShipControl);
        enemyShipControl.setEnemyBehaviour(enemyBehaviour);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
