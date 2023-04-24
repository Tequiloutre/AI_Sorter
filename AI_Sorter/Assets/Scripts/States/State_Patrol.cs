using UnityEngine;

public class State_Patrol : State
{
	private BrainComponent brain = null;
	private MovementComponent movement = null;
	private Vector3 targetPosition = Vector3.zero;
	private bool isWaiting = false;
	private float idleTime = 0.0f,
		targetIdleTime = 1.0f;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, stateInfo, layerIndex);
		brain = character.GetBrain;
		movement = character.GetMovement;
		targetPosition = character.transform.position;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateUpdate(animator, stateInfo, layerIndex);
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
}
