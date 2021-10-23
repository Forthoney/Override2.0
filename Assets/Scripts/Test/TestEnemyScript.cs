using UnityEngine;

public class TestEnemyScript : MonoBehaviour {
	void Awake() {
		GetComponent<ShipControlComponent>().setBody(ShipBodyType.SlowChunk);
		GetComponent<ShipControlComponent>().setWeapon(ShipWeaponType.BulletWeapon);
	}
}