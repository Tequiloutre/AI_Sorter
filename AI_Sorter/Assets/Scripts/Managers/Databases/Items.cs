using System;
using System.Linq;
using UnityEngine;

[Serializable]
public struct ItemDBEntry
{
	public string id;
	public ItemData item;
}

[CreateAssetMenu(fileName = "ItemDB", menuName = "DB/Items", order = 100)]
public class Items : ScriptableObject
{
	[SerializeField] private ItemDBEntry[] content;

	public ItemData GetItem(string _id) => content.FirstOrDefault(_entry => _entry.id == _id).item;

	#if UNITY_EDITOR
	public void GetSprites()
	{
		int _count = content.Length;
		for (int i = 0; i < _count; ++i)
			content[i].item.FindSprite();
	}
	#endif
}
