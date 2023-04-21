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
}
