using UnityEngine;

public class TestEnemyScript : MonoBehaviour
{
  void Awake()
  {
    GetComponent<ShipControlComponent>().setNewBodyFromType(ShipBodyType.SlowChunk);
    GetComponent<ShipControlComponent>().setNewWeaponFromType(ShipWeaponType.BulletWeapon);
    GetComponent<ShipControlComponent>().EnemyBehaviour = new BasicBehaviour(GetComponent<ShipControlComponent>());
  }
}