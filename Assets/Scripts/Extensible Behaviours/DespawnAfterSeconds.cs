using UnityEngine;

public class DespawnAfterSeconds : MonoBehaviour {
	public float seconds;
	Timer life;

	void OnEnable() {
		life = new Timer(seconds);
		life.Start();
	}

	void Update() {
		if (!life) Destroy(gameObject);
	}
}