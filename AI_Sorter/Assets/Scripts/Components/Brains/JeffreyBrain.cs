public class JeffreyBrain : BrainComponent
{
	protected override void Init()
	{
		states.Add(NPCState.Patrol, new State_Patrol(NPCState.Patrol));
		states.Add(NPCState.Interact, new State_Interact(NPCState.Interact));
		base.Init();
		SetActiveState(NPCState.Patrol);
	}
}
