using UnityEngine;

public class TestPlayerScript : MonoBehaviour
{
  void Awake()
  {
    GetComponent<ShipControlComponent>().ShipBody = GameManager.Instance.getBodyFromPool();
    GetComponent<ShipControlComponent>().ShipWeapon = GameManager.Instance.getWeaponFromPool();
  }
}