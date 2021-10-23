using UnityEngine;

public class TestPlayerScript : MonoBehaviour {
	void Awake() {
		GetComponent<ShipControlComponent>().setNewBodyFromType(ShipBodyType.FastSquish);
		GetComponent<ShipControlComponent>().setNewWeaponFromType(ShipWeaponType.BulletWeapon);
	}
}