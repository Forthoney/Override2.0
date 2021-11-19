using UnityEngine;

namespace Thuleanx.Animation {
	public class Wavy : MonoBehaviour {
		public float Amount = 2f;
		public float Period = 1.333f;
		[Range(0f, 1f)]
		public float Offset = 1f;
		float timeStart;
		Vector2 original;

		RectTransform rectTransform;

		private void Awake() {
			rectTransform = GetComponent<RectTransform>();
		}

		private void OnEnable() {
			timeStart = Time.unscaledTime;
			original = transform.localPosition; 
		}

		void Update() {
			transform.localPosition = original + Wave(Time.unscaledTime - timeStart);
		}

		public Vector2 Wave(float time) {
			return new Vector2(0f, Mathf.Sin(2 * Mathf.PI * (Offset + time / Period)));
		}
	}
}