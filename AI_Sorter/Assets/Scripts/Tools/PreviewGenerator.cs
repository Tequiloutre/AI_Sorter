using System.IO;
using UnityEditor;
using UnityEngine;

public static class PreviewGenerator
{
	[MenuItem("Assets/Create/Sprite Preview", false, priority = 310)]
	private static void GeneratePreviewSprite()
	{
		GameObject[] _objects = Selection.gameObjects;
		foreach (GameObject _object in _objects)
		{
			string _assetPath = AssetDatabase.GetAssetPath(_object);
			
			string _localFolder = _assetPath[_assetPath.IndexOf('/')..(_assetPath.LastIndexOf('/') + 1)];
			File.WriteAllBytes(Application.dataPath + _localFolder + _object.name + ".png", AssetPreview.GetAssetPreview(_object).EncodeToPNG());
			
			AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
			
			string _assetFolder = _assetPath[..(_assetPath.LastIndexOf('/') + 1)];
			TextureImporter _importer = (TextureImporter) AssetImporter.GetAtPath(_assetFolder + _object.name + ".png");
			_importer.isReadable = true;
			_importer.textureType = TextureImporterType.Sprite;
			_importer.SaveAndReimport();
		}
		
		AssetDatabase.Refresh();
	}
}
