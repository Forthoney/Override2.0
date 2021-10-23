using UnityEngine;

public class TestPlayerScript : MonoBehaviour {
	void Awake() {
		GetComponent<ShipControlComponent>().setBody(ShipBodyType.FastSquish);
		GetComponent<ShipControlComponent>().setWeapon(ShipWeaponType.BulletWeapon);
	}
}