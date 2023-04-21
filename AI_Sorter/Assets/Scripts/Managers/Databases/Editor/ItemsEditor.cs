using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Items))]
public class ItemsEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Find Sprites")) ((Items) target).GetSprites();
	}
}
