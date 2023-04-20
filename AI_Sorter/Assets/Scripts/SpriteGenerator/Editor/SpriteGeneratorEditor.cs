using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteGenerator))]
public class SpriteGeneratorEditor : Editor
{
	private SpriteGenerator generator = null;
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (!generator) generator = (SpriteGenerator) target;
		
		if (GUILayout.Button("Generate")) generator.Generate();
	}
}
