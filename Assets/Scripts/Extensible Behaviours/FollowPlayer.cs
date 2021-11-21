using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	void Update() {
		if (GameManager.PlayerShip != null) {
			transform.position = GameManager.PlayerShip.transform.position;
			transform.rotation = GameManager.PlayerShip.transform.rotation;
		}
	}
}