using UnityEngine;

public class Entity : MonoBehaviour, IInteractable
{
	public virtual void OnCursorClick() { }
	public virtual void Interact(Entity _entity) { }
}
