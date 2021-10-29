using UnityEngine;

public class BloodVessel : MonoBehaviour {
	public SpriteMask Smask {get; private set; }
	public SpriteRenderer Sprite {get; private set; }

	[Header("Pulsing Info")]
	public float PulseBPM = 60f;
	public float PulseIntensity = 1f;
	public Vector2 VeinTide;

	float _awakeTime;

	void Awake() {
		_awakeTime = Time.time;
		Smask = GetComponentInChildren<SpriteMask>();
		Sprite = GetComponent<SpriteRenderer>();
	}

	void Update() {
		if (Sprite != null && Sprite.material != null)
			Sprite.material.SetFloat("_Intensity", PulseIntensity * CoolFunctions.OneHeartBeat(PulseBPM / 60f * (Time.time - _awakeTime)));

		if (Smask != null) {
			Smask.transform.localScale = Vector3.one * Mathf.Lerp(VeinTide.x, VeinTide.y, CoolFunctions.OneHeartBeat(PulseBPM / 60f * (Time.time - _awakeTime)));
		}
	}
}