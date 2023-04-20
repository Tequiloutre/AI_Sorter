using UnityEngine;

public class Storage : Entity
{
	[SerializeField] private InventoryComponent inventory = null;

	public InventoryComponent GetInventory => inventory;
}
