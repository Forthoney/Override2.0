using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputController : MonoBehaviour
{
  public static InputController Instance;

  [Tooltip("How long are the inputs buffered, in seconds.")]
  public float InputBufferTime = .2f;

	[HideInInspector] public Vector2 Movement;
	[HideInInspector] public Vector2 MouseScreenPos;
	[HideInInspector] public bool Firing = false;
	[HideInInspector] public bool Swapping = false;
	[HideInInspector] public bool Pausing = false;

  public Vector2 MouseWorldPos
  {
    get
    {
      if (Camera.main != null) return Camera.main.ScreenToWorldPoint(MouseScreenPos);
      return Vector2.zero;
    }
  }

  void Awake()
  {
    Instance = this;
  }

  public void OnMoveInput(InputAction.CallbackContext context)
  {
    Movement = context.ReadValue<Vector2>();
  }

  public void OnMouseInput(InputAction.CallbackContext context)
  {
    MouseScreenPos = context.ReadValue<Vector2>();
  }

	public void OnFireInput(InputAction.CallbackContext context) {
		if (context.started)
			Firing = true;
		else if (context.canceled)
			Firing = false;
    }

	public void OnSwapInput(InputAction.CallbackContext context) {
		if (context.started)
			Swapping = true;
		else if (context.canceled)
			Swapping = false;
	}

	public void OnPauseInput(InputAction.CallbackContext context)
    {
		if (context.started)
			Pausing = !Pausing;
    }
}
