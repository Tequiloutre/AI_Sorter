using System.Collections.Generic;
using System.Linq;
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
		Debug.Log("Set Target");
		interactables.Clear();
		interactables.Add(_target);
		activeTarget = _target;
	}

	private void OnTargetDetected(Transform _target)
	{
		if (!isActive) return;
		if (_target.GetComponent<IInteractable>() == null || interactables.Contains(_target)) return;
		interactables.Add(_target);
		CheckNearestTarget();
	}

	private void CheckNearestTarget()
	{
		Vector3 _position = character.transform.position;
		int _count = interactables.Count;
		if (_count == 0) return;
		activeTarget = interactables[0];
		for (int i = 1; i < _count; ++i)
		{
			Transform _target = interactables[i];
			if (Vector3.Distance(_position, _target.position) < Vector3.Distance(_position, activeTarget.position))
				activeTarget = _target;
		}
	}

	public override void Update()
	{
		base.Update();
		if (!activeTarget && interactables.Count == 0)
		{
			brain.SetActiveState(NPCState.Patrol);
			return;
		}
		Vector3 _position = character.transform.position;
		if (Vector3.Distance(_position, activeTarget.position) > interaction.GetRange)
		{
			Vector3 _direction = (activeTarget.position.ResetY() - _position.ResetY()).normalized * (movement.GetMoveSpeed * Time.deltaTime);
			movement.MoveTowards(_direction);
			return;
		}
		activeTarget.GetComponent<IInteractable>().Interact(character);
		RemoveActiveTarget();
	}

	private void RemoveActiveTarget()
	{
		if (!activeTarget) return;
		interactables.Remove(activeTarget);
		activeTarget = null;
		if (interactables.Count > 0) CheckNearestTarget();
	}
}
