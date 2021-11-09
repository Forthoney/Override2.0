using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxSpeedOverride : MonoBehaviour {
	public SpriteRenderer Sprite {get; private set; }
	public float speed = 1;

	void Awake() {
		Sprite = GetComponent<SpriteRenderer>();
		if (Sprite.material != null)
			Sprite.material = Instantiate<Material>(Sprite.material);
		Recalibrate();
	}

	void Update() {
		#if UNITY_EDITOR
			// no need to recalibrate constantly in real game
			Recalibrate();
		#endif
	}

	void Recalibrate() {
		if (Camera.main.transform.localPosition.z != transform.localPosition.z) {
			float speedMult = Mathf.Abs(Camera.main.transform.localPosition.z) / Mathf.Abs(Camera.main.transform.localPosition.z - transform.localPosition.z);
			Vector2 vel = Sprite.material.GetVector("_Velocity");
			Sprite.material.SetVector("_Velocity", (vel == Vector2.zero ? Vector2.zero : vel.normalized) * speed * speedMult);
		}
	}
}