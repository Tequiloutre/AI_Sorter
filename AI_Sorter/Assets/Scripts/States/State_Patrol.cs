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
		if (CheckPosition())
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
			if (!GetNewTargetPosition()) return;
		}
		movement.MoveTowards((targetPosition.ResetY() - character.transform.position.ResetY()).normalized * (movement.GetMoveSpeed * Time.deltaTime));
	}

	private bool GetNewTargetPosition()
	{
		bool _ok = false;
		while (!_ok)
		{
			if (!NavMesh.Instance.GetRandomPoint(out targetPosition)) return false;
			_ok = movement.CheckSafeMove(character.transform.position, targetPosition);
		}
		return true;
	}

	private bool CheckPosition()
	{
		return Vector3.Distance(character.transform.position.ResetY(), targetPosition.ResetY()) <= 0.1f;
	}

	private void OnTargetDetected(Transform _target)
	{
		if (!isActive || !_target.GetComponent<Item>()) return;
		State_Interact _state = (State_Interact) brain.GetState(NPCState.Interact);
		_state.SetTarget(_target);
		brain.SetActiveState(NPCState.Interact);
	}
}
