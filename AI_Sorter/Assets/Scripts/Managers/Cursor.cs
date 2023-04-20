using System;
using UnityEngine;

public class Cursor : Singleton<Cursor>
{
	public event Action<Vector3> OnGroundHit = null;

	[SerializeField] private Camera sceneCamera = null;
	[SerializeField] private LayerMask interactLayer = 0;
	[SerializeField] private float range = 200.0f;

	protected override void PostInit()
	{
		base.PostInit();
		InputManager.Instance.OnInputClick += OnInputClick;
	}

	private void OnInputClick(bool _value)
	{
		if (!_value) return;
		bool _hit = Physics.Raycast(sceneCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit _hitInfo, range, interactLayer);
		if (!_hit) return;
		OnGroundHit?.Invoke(_hitInfo.point);
	}
}
