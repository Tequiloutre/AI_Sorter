using System;
using UnityEngine;

[Serializable]
public abstract class State
{
	public event Action<State> OnStateChanged = null;

	[SerializeField] protected NPCState id = NPCState.None;

	protected bool isActive = false;
	protected BrainComponent brain = null;
	protected Character character = null;
	
	public NPCState GetID => id;

	public State(NPCState _id) => id = _id;

	public void SetActive(bool _value) => isActive = _value;
	
	public virtual void Init(BrainComponent _brain)
	{
		brain = _brain;
		character = brain.GetCharacter;
	}

	public virtual void Enter()
	{
		OnStateChanged?.Invoke(this);
	}
	public virtual void Update() { }
	public virtual void Exit() { }
}
