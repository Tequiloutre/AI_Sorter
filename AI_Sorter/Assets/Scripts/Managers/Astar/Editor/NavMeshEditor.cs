using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NavMesh))]
public class NavMeshEditor : Editor
{
	private NavMesh navMesh = null;
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (!navMesh) navMesh = (NavMesh) target;
		
		if (GUILayout.Button("Generate Ground")) navMesh.GenerateGround();
	}
}
