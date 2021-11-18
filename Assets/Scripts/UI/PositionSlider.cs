using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PositionSlider : MonoBehaviour {
	float _value;
	public float Value {
		get => _value;
		set {
			_value = value;
			if (rectTransform != null)
				rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(slidingExtremes.x, slidingExtremes.y, _value), rectTransform.anchoredPosition.y);
		}
	}

	RectTransform rectTransform;
	public Vector2 slidingExtremes;

	void Awake() {
		rectTransform = GetComponent<RectTransform>();
	}
}