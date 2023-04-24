using UnityEngine;
using Random = UnityEngine.Random;

public class State_Patrol : State
{
	private BrainComponent brain = null;
	private MovementComponent movement = null;
	private SightComponent sight = null;
	private Vector3 targetPosition = Vector3.zero;
	private bool isWaiting = false;
	private float idleTime = 0.0f,
		targetIdleTime = 1.0f;

	public override void Enter()
	{
		base.Enter();
		brain = character.GetBrain;
		movement = character.GetMovement;
		sight = character.GetSight;
		targetPosition = character.transform.position;
	} 

	public override void Update()
	{
		base.Update();
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
		sight.SearchTarget();
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
}
