using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : UI_Window
{
	[SerializeField] private UI_ItemSlot[] uiSlots = null;

	private InventoryComponent inventory = null;

	private void ClearContent()
	{
		foreach (UI_ItemSlot _uiSlot in uiSlots)
			_uiSlot.ResetContent();
	}

	private void RefreshContent(List<ItemSlot> _slots)
	{
		ClearContent();
		int _count = _slots.Count;
		for (int i = 0; i < _count; ++i)
			uiSlots[i].SetContent(_slots[i]);
	}
	
	public void SetContent(InventoryComponent _inventory)
	{
		if (inventory) inventory.OnSlotsModified -= RefreshContent;
		inventory = _inventory;
		inventory.OnSlotsModified += RefreshContent;
		RefreshContent(inventory.GetSlots);
	}
}
