using UnityEngine;

public class Character : Entity
{
	[SerializeField] private string entityName = "Character";
	[SerializeField] private BrainComponent brain = null;
	[SerializeField] private MovementComponent movement = null;
	[SerializeField] private InventoryComponent inventory = null;
	[SerializeField] private SightComponent sight = null;

	public BrainComponent GetBrain => brain;
	public MovementComponent GetMovement => movement;
	public InventoryComponent GetInventory => inventory;
	public SightComponent GetSight => sight;

	public override void Interact(Entity _entity)
	{
		base.Interact(_entity);
		Debug.Log($"[{entityName}] Banjour !");
	}

	public override void OnCursorClick()
	{
		base.OnCursorClick();
		GameHUD.Instance.OpenInventory(inventory);
	}
}
