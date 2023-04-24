using UnityEngine;

public class Character : Entity
{
	[SerializeField] private BrainComponent brain = null;
	[SerializeField] private MovementComponent movement = null;
	[SerializeField] private InventoryComponent inventory = null;

	public BrainComponent GetBrain => brain;
	public MovementComponent GetMovement => movement;
	public InventoryComponent GetInventory => inventory;

	public override void OnCursorClick()
	{
		base.OnCursorClick();
		GameHUD.Instance.OpenInventory(inventory);
	}
}
