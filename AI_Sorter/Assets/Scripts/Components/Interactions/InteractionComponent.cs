using UnityEngine;

public class InteractionComponent : Component
{
	[SerializeField] private float range = 2.0f;

	public float GetRange => range;
}
