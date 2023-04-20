using UnityEngine;

public class Character : Entity
{
	[SerializeField] private InventoryComponent inventory = null;

	public InventoryComponent GetInventory => inventory;

	public override void OnCursorClick()
	{
		base.OnCursorClick();
		GameHUD.Instance.OpenInventory(inventory);
	}
}
