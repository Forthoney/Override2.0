using UnityEngine;

public class ParticleCombo : MonoBehaviour {
	public void Play() {
		foreach (Transform child in transform)
			child.GetComponent<ParticleSystem>()?.Play();
	}

	public void PlayOnce() {
		Play();
		float maxDuration = 0;
		foreach (Transform child in transform) {
			ParticleSystem system = child.GetComponent<ParticleSystem>();
			maxDuration = Mathf.Max(maxDuration, system.main.duration);
			if (system.main.duration >= maxDuration)
				maxDuration = 1;
		}
		Invoke("_Destroy", maxDuration);
	}

	void _Destroy() {
		Destroy(gameObject);
	}
}