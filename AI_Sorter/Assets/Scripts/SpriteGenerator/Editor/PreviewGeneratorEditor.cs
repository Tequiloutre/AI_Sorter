using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PreviewGenerator))]
public class PreviewGeneratorEditor : Editor
{
	private PreviewGenerator generator = null;
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (!generator) generator = (PreviewGenerator) target;
		
		if (GUILayout.Button("Generate")) generator.Generate();
	}
}
