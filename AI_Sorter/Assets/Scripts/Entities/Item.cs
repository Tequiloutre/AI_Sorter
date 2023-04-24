using UnityEngine;

public class Item : Entity
{
	[SerializeField] private string item = "Item";
	[SerializeField] private bool canTake = true;

	public override void Interact(Entity _entity)
	{
		base.Interact(_entity);
		Character _character = (Character) _entity;
		if (!_character) return;
		TakeItem(_character);
	}

	private void TakeItem(Character _character)
	{
		_character.GetInventory.AddItem(DB.Instance.Items.GetItem(item));
		Destroy(gameObject);
	}
}
