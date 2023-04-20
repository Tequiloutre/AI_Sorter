using UnityEngine;

public class Character : Entity
{
	[SerializeField] private InventoryComponent inventory = null;

	public InventoryComponent GetInventory => inventory;
}
