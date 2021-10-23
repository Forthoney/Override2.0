using UnityEngine;

public class TestEnemyScript : MonoBehaviour {
	void Awake() {
		ShipControlComponent control = GetComponent<ShipControlComponent>();
		control.setBody(new SlowChunk());
		control.setWeapon(new BulletWeapon("Prefabs/Bullet", Vector2.zero, gameObject));
		control.setEnemyBehaviour(new BasicBehaviour(control));
	}
}