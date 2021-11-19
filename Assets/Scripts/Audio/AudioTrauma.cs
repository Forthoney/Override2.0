using UnityEngine;

public class AudioTrauma : MonoBehaviour {
	[SerializeField] float recovery = 1f;
	float trauma = 0;

	public void AddTrauma(float amt) {
		trauma += amt;
	}

	void Update() {
		trauma = Mathf.Clamp01(trauma - Time.unscaledDeltaTime * recovery);
		FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Hit", trauma);
	}
}