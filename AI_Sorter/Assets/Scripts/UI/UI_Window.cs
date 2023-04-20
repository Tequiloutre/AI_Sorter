using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UI_Window : MonoBehaviour
{
	[SerializeField] protected UI_Window parentWindow = null;
	[SerializeField] protected List<UI_Window> childWindows = new List<UI_Window>();
	[SerializeField] protected bool keepFirstWindow = true;
	
	protected CanvasGroup canvas = null;
	protected bool isOpened = false;

	protected CanvasGroup Canvas => canvas ? canvas : canvas = GetComponent<CanvasGroup>();
	public bool IsOpened => isOpened;

	private bool HasChildOpened() => childWindows.Any(_childWindow => _childWindow.isOpened);
	private void OnChildWindowClosed()
	{
		if (keepFirstWindow && !childWindows.Any(_childWindow => _childWindow.isOpened))
			childWindows[0].Open();
	}
	private void CloseChildWindowsExcept(UI_Window _window)
	{
		foreach (UI_Window _childWindow in childWindows.Where(_childWindow => _childWindow != _window))
			_childWindow.Close(true);
	}

	public virtual void Open()
	{
		GameHUD.Instance.SetActiveWindow(this);
		isOpened = true;
		Canvas.alpha = 1;
		if (parentWindow)
		{
			if (!parentWindow.isOpened) parentWindow.Open();
			parentWindow.CloseChildWindowsExcept(this);
		}
		if (childWindows.Count > 0 && keepFirstWindow && !HasChildOpened()) childWindows[0].Open();
	}
	public virtual void Close(bool _ignoreParent = false)
	{
		isOpened = false;
		Canvas.alpha = 0;
		foreach (UI_Window _childWindow in childWindows)
			_childWindow.Close(_ignoreParent);
		if (!_ignoreParent && parentWindow) parentWindow.OnChildWindowClosed();
	}
	public void Switch()
	{
		if (isOpened) Close();
		else Open();
	}
}
