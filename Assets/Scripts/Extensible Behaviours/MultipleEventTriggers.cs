using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class MultipleEventTriggers : MonoBehaviour {
	[SerializeField] List<UnityEvent> events = new List<UnityEvent>();
	public void Trigger(int i) {
		if (i >= 0 && i < events.Count)
			events[i]?.Invoke();
	}
}