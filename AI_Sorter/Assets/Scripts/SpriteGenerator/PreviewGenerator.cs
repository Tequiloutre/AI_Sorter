using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PreviewGenerator : MonoBehaviour
{
	[SerializeField] private string sourcePath = "/Prefabs/";
	[SerializeField] private string targetPath = "/Prefabs/";
	[SerializeField] private bool textureAsSprite = true;

	public void Generate()
	{
		Debug.Log($"[SpriteGenerator] Generating Sprites from {"Assets" + sourcePath}");
		
		string[] _sourceFiles = Directory.GetFiles(Application.dataPath + "/" + sourcePath);
		Debug.Log($"[SpriteGenerator] Found {_sourceFiles.Length} files");
		foreach (string _file in _sourceFiles)
		{
			int index = _file.LastIndexOf("/", StringComparison.Ordinal);
			string _localPath = "Assets" + sourcePath;

			if (index > 0) _localPath += _file.Substring(index + 1);
			
			GameObject _object = AssetDatabase.LoadAssetAtPath<GameObject>(_localPath);
			if (!_object) continue;

			File.WriteAllBytes(Application.dataPath + targetPath + _object.name + ".png", AssetPreview.GetAssetPreview(_object).EncodeToPNG());

			if (!textureAsSprite) continue;
			
			AssetDatabase.Refresh();

			TextureImporter _importer = (TextureImporter) AssetImporter.GetAtPath("Assets" + targetPath + _object.name + ".png");
			_importer.isReadable = true;
			_importer.textureType = TextureImporterType.Sprite;
			_importer.SaveAndReimport();
		}
		
		AssetDatabase.Refresh();
		
		Debug.Log("[SpriteGenerator] Generation done");
	}
}
