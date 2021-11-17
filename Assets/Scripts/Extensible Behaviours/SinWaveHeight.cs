using UnityEngine;

public class SinWaveHeight : MonoBehaviour {
	[SerializeField] float Frequency = 1f;
	[SerializeField] float Amplitude = 1f;

	Vector2 orig;

	void Awake() {
		orig = transform.localPosition;
	}

	void Update() {
		transform.localPosition = (Vector3) orig + new Vector3(
			transform.localPosition.x, 
			Mathf.Sin(Mathf.PI * 2 * Frequency * Time.time)*Amplitude,
			transform.localPosition.z
		);
	}
}