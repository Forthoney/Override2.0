using UnityEngine;
using UnityEngine.Events;

public class TriggerOnPlayerDamage : MonoBehaviour {
	public UnityEvent OnPlayerDamage;

	void Start() {
		PlayerControl.Instance.OnDamageTaken?.AddListener(() => {
			OnPlayerDamage?.Invoke();
		});
	}
}