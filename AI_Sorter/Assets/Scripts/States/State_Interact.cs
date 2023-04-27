using System.Collections.Generic;
using UnityEngine;

public class State_Interact : State
{
	private SightComponent sight = null;
	private InteractionComponent interaction = null;
	private MovementComponent movement = null;
	private List<Transform> interactables = new List<Transform>();
	private Transform activeTarget = null;

	public State_Interact(NPCState _id) : base(_id) { }

	public override void Init(BrainComponent _brain)
	{
		base.Init(_brain);
		sight = character.GetSight;
		sight.OnTargetDetected += OnTargetDetected;
		interaction = character.GetInteraction;
		movement = character.GetMovement;
	}

	public void SetTarget(Transform _target)
	{
		interactables.Clear();
		interactables.Add(_target);
	}

	public override void Update()
	{
		base.Update();
		
		sight.SearchTarget();
		
		if (!activeTarget && !GetNearestTarget())
		{
			brain.SetActiveState(NPCState.Store);
			return;
		}
		
		Vector3 _position = character.transform.position;
		
		if (Vector3.Distance(_position, activeTarget.position) <= interaction.GetRange)
		{
			activeTarget.GetComponent<IInteractable>().Interact(character);
			RemoveActiveTarget();
		}
		
		if (!brain.GetNextPathPoint(_position, out Vector3 _targetPosition)) return;
		Vector3 _direction = (_targetPosition.ResetY() - _position.ResetY()).normalized * (movement.GetMoveSpeed * Time.deltaTime);
		movement.MoveTowards(_direction);
	}
	
	private bool GetNearestTarget()
	{
		Transform _previousTarget = activeTarget;
		int _count = interactables.Count;
		if (_count == 0)
		{
			activeTarget = null;
			return false;
		}
		
		Vector3 _position = character.transform.position;
		activeTarget = interactables[0];
		for (int i = 1; i < _count; ++i)
		{
			Transform _target = interactables[i];
			if (Vector3.Distance(_position, _target.position) < Vector3.Distance(_position, activeTarget.position))
				activeTarget = _target;
		}

		if (activeTarget == _previousTarget) return true;
		
		brain.ComputePath(_position, activeTarget.position);
		brain.ResetPathIndex();
		return true;
	}
	
	private void OnTargetDetected(Transform _target)
	{
		if (!isActive) return;
		if (_target.GetComponent<IInteractable>() == null || interactables.Contains(_target)) return;
		interactables.Add(_target);
		GetNearestTarget();
	}

	private void RemoveActiveTarget()
	{
		if (!activeTarget) return;
		interactables.Remove(activeTarget);
		activeTarget = null;
	}
}
