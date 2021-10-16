using UnityEngine;

public class TestEnemyScript : MonoBehaviour {
	void Awake() {
		GetComponent<ShipControlComponent>().setBody(new SlowChunk());
		GetComponent<ShipControlComponent>().setWeapon(new BulletWeapon("Prefabs/Bullet", Vector2.zero, gameObject));
	}
}