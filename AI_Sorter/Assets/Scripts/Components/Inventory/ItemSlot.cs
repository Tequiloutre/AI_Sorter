using System;

[Serializable]
public class ItemSlot
{
	public ItemData Item;
	public int Count;

	public ItemSlot(ItemData _item)
	{
		Item = _item;
		Count = 1;
	}
}
