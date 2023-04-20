using System;
using UnityEngine;

public class GameHUD : UI_Window
{
	[SerializeField] private Camera screenCamera = null;
	[SerializeField] private UI_Window hud = null;
	[SerializeField] private UI_Inventory inventory = null;

	private static GameHUD instance = null;
	private UI_Window activeWindow = null;
	private Vector3 activeWindowAnchor = Vector3.zero;

	public static GameHUD Instance => instance;

	private void Awake()
	{
		if (instance)
		{
			Destroy(this);
			return;
		}

		instance = this;
	}

	private void Start()
	{
		Cursor.Instance.OnGroundHit += OnGroundHit;
	}

	private void OnGroundHit(Vector3 _position)
	{
		if (activeWindow) activeWindow.Close();
	}

	public void SetActiveWindow(UI_Window _window)
	{
		if (_window == this || _window == hud) return;
		activeWindow = _window;
	}

	public Vector3 GetScreenPosition(Vector3 _worldPosition)
	{
		return screenCamera.WorldToScreenPoint(_worldPosition + Vector3.up * 3.0f);
	}

	private void LateUpdate()
	{
		if (activeWindow)
		{
			activeWindow.transform.position = GetScreenPosition(activeWindowAnchor);
		}
	}

	public void OpenInventory(InventoryComponent _inventory)
	{
		activeWindowAnchor = _inventory.transform.position;
		inventory.transform.position = GetScreenPosition(activeWindowAnchor);
		inventory.Open();
		inventory.SetContent(_inventory);
	}
}
