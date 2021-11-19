using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DespawnOnTrigger : MonoBehaviour {
	public float delay = 3f;

	public void TriggerDespawn() => StartCoroutine(_DespawnDelayed(delay));
	IEnumerator _DespawnDelayed(float delay) {
		yield return new WaitForSecondsRealtime(delay);
		gameObject.SetActive(false);
	}
}