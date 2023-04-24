public class JeffreyBrain : BrainComponent
{
	protected override void Init()
	{
		states.Add(NPCState.Patrol, new State_Patrol());
		base.Init();
		SetActiveState(NPCState.Patrol);
	}
}
