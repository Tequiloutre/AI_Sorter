using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public static class PreviewGenerator
{
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Sprite Preview", false, priority = 310)]
	private static void GeneratePreviewSprite()
	{
		GameObject[] _objects = Selection.gameObjects;
		foreach (GameObject _object in _objects)
			GenerateObjectSprite(_object);
		
		AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
	}

	private static void GenerateObjectSprite(GameObject _object)
	{
		string _assetPath = AssetDatabase.GetAssetPath(_object);
		
		Texture2D _texture = null;
		while (_texture == null)
			_texture = AssetPreview.GetAssetPreview(_object);
		
		byte[] _data = _texture.EncodeToPNG();
		string _localFolder = _assetPath[_assetPath.IndexOf('/')..(_assetPath.LastIndexOf('/') + 1)];
		File.WriteAllBytes(Application.dataPath + _localFolder + _object.name + ".png", _data);
			
		AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
			
		string _assetFolder = _assetPath[..(_assetPath.LastIndexOf('/') + 1)];
		TextureImporter _importer = (TextureImporter) AssetImporter.GetAtPath(_assetFolder + _object.name + ".png");
		_importer.isReadable = true;
		_importer.textureType = TextureImporterType.Sprite;
		_importer.SaveAndReimport();
	}
	#endif
}
