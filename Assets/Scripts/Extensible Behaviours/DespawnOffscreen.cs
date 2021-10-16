using UnityEngine;

public class DespawnOffscreen : MonoBehaviour {
	public static int FRAMES_PER_CHECK = 10;

	Collider2D _collider;
	int _framesSinceCheck;

	void Awake() { 
		_collider = GetComponent<Collider2D>();
	}

	void Update() {
		if (++_framesSinceCheck >= FRAMES_PER_CHECK) {
			Vector2 pos = Camera.main.WorldToViewportPoint(transform.position);
			if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
				gameObject.SetActive(false);
			_framesSinceCheck = 0;
		}
	}
}