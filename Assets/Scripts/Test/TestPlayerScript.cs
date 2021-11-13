using UnityEngine;

public class TestPlayerScript : MonoBehaviour
{
  public ShipBody startBody;
  public ShipWeapon startWeapon;
  void Awake()
  {
    GetComponent<ShipControlComponent>().ShipBody = Instantiate(startBody);
    GetComponent<ShipControlComponent>().ShipWeapon = Instantiate(startWeapon);
  }
}