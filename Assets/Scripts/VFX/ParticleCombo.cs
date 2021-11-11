using UnityEngine;

public class ParticleCombo : MonoBehaviour {
	public void Play() {
		foreach (Transform child in transform)
			child.GetComponent<ParticleSystem>()?.Play();
	}
}