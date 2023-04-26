using UnityEngine;
using Random = UnityEngine.Random;

public class State_Patrol : State
{
	private MovementComponent movement = null;
	private SightComponent sight = null;
	private Vector3 targetPosition = Vector3.zero;
	private bool isWaiting = false;
	private float idleTime = 0.0f,
		targetIdleTime = 1.0f;
	
	public State_Patrol(NPCState _id) : base(_id) { }

	public override void Init(BrainComponent _brain)
	{
		base.Init(_brain);
		movement = character.GetMovement;
		sight = character.GetSight;
		sight.OnTargetDetected += OnTargetDetected;
	}

	public override void Enter()
	{
		base.Enter();
		targetPosition = character.transform.position;
	} 

	public override void Update()
	{
		base.Update();
		sight.SearchTarget();
		
		Vector3 _position = character.transform.position;

		if (!brain.GetNextPathPoint(_position, out Vector3 _targetPosition))
		{
			if (!isWaiting)
			{
				targetIdleTime = Random.Range(brain.GetMinIdleTime, brain.GetMaxIdleTime);
				isWaiting = true;
			}
			if (idleTime < targetIdleTime)
			{
				idleTime += Time.deltaTime;
				movement.Move(Vector3.zero);
				return;
			}
			idleTime = 0.0f;
			isWaiting = false;
			
			brain.ComputePath(_position, Room.Instance.GetRandomPoint());
			brain.ResetPathIndex();
			return;
		}
		
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
