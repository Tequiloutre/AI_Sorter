using System;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
	public event Action<float> OnInputVertical = null,
		OnInputHorizontal = null,
		OnInputMouseWheel = null;

	public event Action<bool> OnInputClick = null; 

	private void Update()
	{
		OnInputVertical?.Invoke(Input.GetAxis("Vertical"));
		OnInputHorizontal?.Invoke(Input.GetAxis("Horizontal"));
		OnInputMouseWheel?.Invoke(Input.mouseScrollDelta.y);
		
		OnInputClick?.Invoke(Input.GetMouseButtonDown(0));
	}
}
