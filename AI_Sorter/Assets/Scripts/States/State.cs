using System;
using UnityEngine;

public abstract class State : StateMachineBehaviour
{
	public event Action<State> OnStateChanged = null;

	[SerializeField] private string stateName = "State";

	protected Character character = null;

	public string Name => stateName;
	
	public void Init(BrainComponent _brain)
	{
		character = _brain.GetCharacter;
	}

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter(animator, stateInfo, layerIndex);
		OnStateChanged?.Invoke(this);
	}
}
