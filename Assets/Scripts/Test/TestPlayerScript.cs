using UnityEngine;

public class TestPlayerScript : MonoBehaviour {
	void Awake() {
		GetComponent<ShipControlComponent>().setBody(new FastSquish());
		GetComponent<ShipControlComponent>().setWeapon(new BulletWeapon("Prefabs/Bullet", Vector2.zero));
	}
}