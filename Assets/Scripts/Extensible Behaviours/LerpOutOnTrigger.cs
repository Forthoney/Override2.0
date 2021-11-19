using UnityEngine;
using System.Collections;

public class LerpOutOnTrigger : MonoBehaviour {
	public float delay = 3f;
	public Vector2 Displacement;
	public float duration;

	public void LerpOut() => StartCoroutine(Out(delay));
	IEnumerator Out(float delay) {
		yield return new WaitForSecondsRealtime(delay);
		Timer timer = new Timer(duration);
		timer.Start();

		Transform trans = GetComponent<Transform>();

		Vector3 src = trans.localPosition;
		Vector3 dest = src + (Vector3) Displacement;
		while (timer) {
			Vector3 cur = Vector3.Lerp(src, dest, 
				Easing.EaseOutCirc(1 - timer.TimeLeft/duration, 1f)
			);
			trans.localPosition = cur;
			yield return null;
		}
	}
}