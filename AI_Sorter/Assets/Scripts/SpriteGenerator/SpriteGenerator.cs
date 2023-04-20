using System.IO;
using UnityEditor;
using UnityEngine;

public class SpriteGenerator : MonoBehaviour
{
	[SerializeField] private string sourcePath = "/Prefabs/";
	[SerializeField] private string targetPath = "/Prefabs/";

	public void Generate()
	{
		string _sourcePath = "Assets" + sourcePath;
		Debug.Log($"[SpriteGenerator] Generating Sprites from {_sourcePath}");

		string[] _files = Directory.GetFiles(Application.dataPath + "/" + sourcePath);
		Debug.Log($"[SpriteGenerator] Found {_files.Length} files");
		foreach (string _file in _files)
		{
			int index = _file.LastIndexOf("/");
			string _localPath = "Assets" + sourcePath;
			
			if (index > 0) _localPath += _file.Substring(index);

			GameObject _object = AssetDatabase.LoadAssetAtPath<GameObject>(_localPath);
			if (!_object) continue;
			
			File.WriteAllBytes(Application.dataPath + targetPath + _object.name + ".png", AssetPreview.GetAssetPreview(_object).EncodeToPNG());
		}
		
		AssetDatabase.Refresh();
		
		Debug.Log("[SpriteGenerator] Generation done");
	}
}
