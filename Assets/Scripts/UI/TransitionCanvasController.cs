using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class TransitionCanvasController : MonoBehaviour {
	public UnityEvent OnHideComplete, OnUnhideComplete;

	public void StartHide() => GetComponent<Animator>().SetTrigger("Hide");
	public void StartUnhide() => GetComponent<Animator>().SetTrigger("Unhide");

	public void OnHideCompleted() => OnHideComplete?.Invoke();
	public void OnUnhideCompleted() => OnUnhideComplete?.Invoke();
}