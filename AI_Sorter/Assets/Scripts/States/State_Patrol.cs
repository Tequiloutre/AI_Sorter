using UnityEngine;

public class State_Patrol : State
{
	private MovementComponent movement = null;
	private Vector3 targetPosition = Vector3.zero;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, stateInfo, layerIndex);
		movement = character.GetMovement;
		targetPosition = character.transform.position;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		if (CheckPosition() && !NavMesh.Instance.GetRandomPoint(out targetPosition)) return;
		Debug.Log($"{character.transform.position} => {targetPosition}");
		movement.MoveTowards((targetPosition.ResetY() - character.transform.position.ResetY()).normalized * (movement.GetMoveSpeed * Time.deltaTime));
	}

	private bool CheckPosition()
	{
		return Vector3.Distance(character.transform.position.ResetY(), targetPosition.ResetY()) <= 0.1f;
	}
}
