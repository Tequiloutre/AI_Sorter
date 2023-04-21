using System;
using UnityEngine;

public class TestTool : MonoBehaviour
{
	[SerializeField] private InventoryComponent testInventory = null;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P)) AddItem();
	}

	public void AddItem()
	{
		testInventory.AddItem(DB.Instance.Items.GetItem("Can"));
	}
}
