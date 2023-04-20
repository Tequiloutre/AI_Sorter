using System;
using UnityEngine;

[Serializable]
public class ItemData
{
	[SerializeField] private string itemName = "Item";
	[SerializeField] private Item item = null;

	public string GetName => itemName;
	public Item GetItem => item;
}
