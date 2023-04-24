using System;
using UnityEngine;

[Serializable]
public abstract class State
{
	public event Action<State> OnStateChanged = null;

	[SerializeField] protected NPCState id = NPCState.None;

	protected bool isActive = false;
	protected Character character = null;
	
	public NPCState GetID => id;

	public void SetActive(bool _value) => isActive = _value;
	
	public void Init(BrainComponent _brain)
	{
		character = _brain.GetCharacter;
	}

	public virtual void Enter()
	{
		OnStateChanged?.Invoke(this);
	}
	public virtual void Update() { }
	public virtual void Exit() { }
}
