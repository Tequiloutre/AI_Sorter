using UnityEngine;

public class BrainComponent : Component
{
	[SerializeField] protected Animator fsm = null;
	[SerializeField] protected float minIdleTime = 1.0f,
		maxIdleTime = 5.0f;
	
	public Character GetCharacter => (Character) entity;
	public float GetMinIdleTime => minIdleTime;
	public float GetMaxIdleTime => maxIdleTime;

	// private void OnAskInteract(IInteractable _interactable) => TriggerEvent(NPCEvent.OnAskInteract);

	protected void TriggerEvent(int _event)
	{
		fsm.SetTrigger(_event);
	}
	
	protected override void Init()
	{
		base.Init();
		
		State[] _states = fsm.GetBehaviours<State>();
		foreach (State _state in _states)
		{
			_state.OnStateChanged += OnStateChanged;
			_state.Init(this);
		}
		
		// NPC.NPCInteraction.OnAskInteract += OnAskInteract;
	}

	private void OnStateChanged(State _state)
	{
		if (debug) Debug.Log($"[StateMachine] Enter state : {_state.Name}");
	}
}
