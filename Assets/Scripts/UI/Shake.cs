using UnityEngine;

public class Shake : MonoBehaviour {
	public float Trauma = 0f;


	RectTransform _rectTransform; 

	[SerializeField] float traumaDecayRate = .4f;
	[SerializeField] float translationalOffsetMax = 2f;
	[SerializeField] float maxShakeAngle = 30f;
	[SerializeField] float NoiseSampleRate = 1f;
	Vector2 origin;
	Quaternion originRot;
	float PerlinNegOneToOne(float seed) => 1 - 2 * Mathf.PerlinNoise(seed, Time.time * NoiseSampleRate);


	float seed;

	public virtual void Awake() {
		seed = Random.Range(0f, 1f);
		_rectTransform = GetComponent<RectTransform>();
		origin = _rectTransform.anchoredPosition;
		originRot = _rectTransform.rotation;
	}

	public void AddTrauma(float amt) {
		Trauma = Mathf.Clamp01(amt + Trauma);
	}

	public void ResetTrauma() => Trauma = 0f;

	void Update() {
		if (!Mathf.Approximately(Trauma, 0f)) {

			float shake = Trauma * Trauma;

			float angle = maxShakeAngle * shake * PerlinNegOneToOne(seed);
			float offsetX = translationalOffsetMax * shake * PerlinNegOneToOne(seed + 1);
			float offsetY = translationalOffsetMax * shake * PerlinNegOneToOne(seed + 2);

			_rectTransform.anchoredPosition = new Vector2(
				offsetX, offsetY
			) + origin;
			_rectTransform.rotation = originRot * Quaternion.Euler(0,0,angle);
			Trauma = Mathf.Clamp01(Trauma - traumaDecayRate * Time.deltaTime);
		} else {
			_rectTransform.anchoredPosition = origin;
			_rectTransform.rotation = originRot;
		}
	}
}