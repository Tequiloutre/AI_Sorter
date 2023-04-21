using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class ItemData
{
	[SerializeField] private string itemName = "Item";
	[SerializeField] private Item item = null;
	[SerializeField] private Sprite sprite = null;

	public string GetName => itemName;
	public Item GetItem => item;
	public Sprite GetSprite => sprite;

	#if UNITY_EDITOR
	public void FindSprite()
	{
		string _assetPath = AssetDatabase.GetAssetPath(item.gameObject);
		string _assetFolder = _assetPath[..(_assetPath.LastIndexOf('/') + 1)];
		sprite = AssetDatabase.LoadAssetAtPath<Sprite>(_assetFolder + item.gameObject.name + ".png");
	}
	#endif
}
