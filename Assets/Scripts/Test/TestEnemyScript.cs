using UnityEngine;

public class TestEnemyScript : MonoBehaviour
{
  void Awake()
  {
    GetComponent<ShipControlComponent>().EnemyBehaviour = new BasicBehaviour(GetComponent<ShipControlComponent>());
  }
}