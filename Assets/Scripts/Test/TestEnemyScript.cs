using UnityEngine;

public class TestEnemyScript : MonoBehaviour {
	void Awake() {
		GetComponent<ShipControlComponent>().setNewBodyFromType(ShipBodyType.SlowChunk);
		GetComponent<ShipControlComponent>().setNewWeaponFromType(ShipWeaponType.BulletWeapon);
	}
}