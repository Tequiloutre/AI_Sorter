using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : Component
{
	[SerializeField] private List<ItemSlot> slots = new List<ItemSlot>();

	public List<ItemSlot> GetSlots => slots;

	public bool Contains(ItemData _item, out int _index)
	{
		int _count = slots.Count;
		for (int i = 0; i < _count; ++i)
		{
			if (slots[i].Item != _item) continue;
			_index = i;
			return true;
		}
		_index = -1;
		return false;
	}

	public void AddItem(ItemData _item)
	{
		if (Contains(_item, out int _index)) ++slots[_index].Count;
		else slots.Add(new ItemSlot(_item));
	}

	public void RemoveItem(ItemSlot _slot)
	{
		--_slot.Count;
		if (_slot.Count > 0) return;
		slots.Remove(_slot);
	}

	public void RemoveItem(int _index)
	{
		if (_index >= slots.Count) return;
		RemoveItem(slots[_index]);
	}
}
