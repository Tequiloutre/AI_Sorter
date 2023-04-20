using System;
using UnityEngine;

public class Cursor : Singleton<Cursor>
{
	public event Action<Vector3> OnGroundHit = null;

	[SerializeField] private Camera sceneCamera = null;
	[SerializeField] private LayerMask entityLayer = 0,
		groundLayer = 0;
	[SerializeField] private float range = 200.0f;

	protected override void PostInit()
	{
		base.PostInit();
		InputManager.Instance.OnInputClick += OnInputClick;
	}

	private void OnInputClick(bool _value)
	{
		if (!_value) return;
		Ray _ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(_ray, out RaycastHit _hitInfo, range, entityLayer))
		{
			_hitInfo.transform.GetComponent<Entity>().OnCursorClick();
			return;
		}
		if (Physics.Raycast(_ray, out _hitInfo, range, groundLayer))
		{
			OnGroundHit?.Invoke(_hitInfo.point);
			return;
		}
	}
}
