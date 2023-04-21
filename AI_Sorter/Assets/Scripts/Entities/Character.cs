using UnityEngine;

public class Character : Entity
{
	[SerializeField] private MovementComponent movement = null;
	[SerializeField] private InventoryComponent inventory = null;

	public MovementComponent GetMovement => movement;
	public InventoryComponent GetInventory => inventory;

	public override void OnCursorClick()
	{
		base.OnCursorClick();
		GameHUD.Instance.OpenInventory(inventory);
	}
}
