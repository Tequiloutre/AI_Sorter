using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestTool))]
public class TestToolEditor : Editor
{
	private TestTool testTool = null;
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (!testTool) testTool = (TestTool) target;
		
		if (GUILayout.Button("Add item")) testTool.AddItem();
	}
}
