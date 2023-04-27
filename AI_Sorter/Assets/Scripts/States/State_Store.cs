using UnityEngine;

public class State_Store : State
{
	private MovementComponent movement = null;
	private InteractionComponent interaction = null;
	private SightComponent sight = null;
	private InventoryComponent inventory = null;
	private Storage storage = null;
	
	public State_Store(NPCState _id) : base(_id) { }

	public override void Init(BrainComponent _brain)
	{
		base.Init(_brain);
		movement = character.GetMovement;
		interaction = character.GetInteraction;
		sight = character.GetSight;
		sight.OnTargetDetected += OnTargetDetected;
		inventory = character.GetInventory;
	}

	public override void Enter()
	{
		base.Enter();
		Vector3 _position = character.transform.position;
		storage = FurnitureManager.Instance.GetNearestStorage(_position);
		brain.ComputePath(_position, storage.transform.position);
		brain.ResetPathIndex();
	}

	public override void Update()
	{
		base.Update();
		
		sight.SearchTarget();

		if (inventory.GetTotalCount == 0)
		{
			brain.SetActiveState(NPCState.Patrol);
			return;
		}

		Vector3 _position = character.transform.position;
		Vector3 _storagePosition = storage.transform.position;
		
		if (Vector3.Distance(_position, _storagePosition) <= interaction.GetRange)
		{
			movement.MoveTowards(Vector3.zero);
			storage.GetInventory.AddItem(inventory.GetSlots[0].Item);
			inventory.RemoveItem(0);
			return;
		}
		
		if (!brain.GetNextPathPoint(_position, out Vector3 _targetPosition)) return;
		Vector3 _direction = (_targetPosition.ResetY() - _position.ResetY()).normalized * (movement.GetMoveSpeed * Time.deltaTime);
		movement.MoveTowards(_direction);
	}

	private void OnTargetDetected(Transform _target)
	{
		if (!isActive || !_target.GetComponent<Item>()) return;
		State_Interact _state = (State_Interact) brain.GetState(NPCState.Interact);
		_state.SetTarget(_target);
		brain.SetActiveState(NPCState.Interact);
	}
}
