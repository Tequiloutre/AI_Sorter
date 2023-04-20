using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UI_Window), true), CanEditMultipleObjects]
public class UI_WindowEditor : Editor
{
	public override void OnInspectorGUI()
	{
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Open")) ((UI_Window) target).Open();
		if (GUILayout.Button("Close")) ((UI_Window) target).Close();
		GUILayout.EndHorizontal();
		base.OnInspectorGUI();
	}
}
