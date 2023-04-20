using UnityEngine;

public class DB : Singleton<DB>
{
	[SerializeField] private Items items = null;

	public Items Items => items;
}
