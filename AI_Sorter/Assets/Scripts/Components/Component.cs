using UnityEngine;

public class Component : MonoBehaviour
{
	[SerializeField] private Entity entity = null;

	public Entity GetEntity => entity;
}
