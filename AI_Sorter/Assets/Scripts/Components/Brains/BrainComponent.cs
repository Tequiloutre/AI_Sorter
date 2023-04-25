using System.Collections.Generic;
using UnityEngine;

public class BrainComponent : Component
{
	// [SerializeField] protected Animator fsm = null;
	[SerializeField] protected float minIdleTime = 1.0f,
		maxIdleTime = 5.0f;

	protected Dictionary<NPCState, State> states = new Dictionary<NPCState, State>();
	protected State activeState = null;
	// private Dictionary<NPCState, State> states = new Dictionary<NPCState, State>();

	public Character GetCharacter => (Character) entity;
	public float GetMinIdleTime => minIdleTime;
	public float GetMaxIdleTime => maxIdleTime;
	public State GetState(NPCState _stateID) => states[_stateID];
	
	// private void OnAskInteract(IInteractable _interactable) => TriggerEvent(NPCEvent.OnAskInteract);

	// protected void TriggerEvent(int _event)
	// {
		// fsm.SetTrigger(_event);
	// }
	
	protected override void Init()
	{
		base.Init();

		foreach (State _state in states.Values)
		{
			_state.OnStateChanged += OnStateChanged;
			_state.Init(this);
		}
		// State[] _states = fsm.GetBehaviours<State>();
		// foreach (State _state in _states)
		// {
		// 	_state.OnStateChanged += OnStateChanged;
		// 	_state.Init(this);
		// }
		
		// NPC.NPCInteraction.OnAskInteract += OnAskInteract;
	}

	public void SetActiveState(NPCState _stateID)
	{
		if (!states.ContainsKey(_stateID)) return;
		if (activeState != null)
		{
			activeState.Exit();
			activeState.SetActive(false);
		}
		activeState = states[_stateID];
		activeState.SetActive(true);
		activeState.Enter();
	}

	private void Update()
	{
		activeState?.Update();
	}

	private void OnStateChanged(State _state)
	{
		if (debug) Debug.Log($"[StateMachine] Enter state : {_state.GetID}");
	}
}
