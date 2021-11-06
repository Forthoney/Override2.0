using UnityEngine;

public class TestPlayerScript : MonoBehaviour
{
  void Awake()
  {
    Debug.Log(GameManager.Instance.getBodyFromPool());
    GetComponent<ShipControlComponent>().ShipBody = GameManager.Instance.getBodyFromPool();
    GetComponent<ShipControlComponent>().ShipWeapon = GameManager.Instance.getWeaponFromPool();
    GetComponent<ShipControlComponent>().ShipWeapon.FiringSource = gameObject;
  }
}