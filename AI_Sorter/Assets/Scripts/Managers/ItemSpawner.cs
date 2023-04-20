using UnityEngine;

public class ItemSpawner : Singleton<ItemSpawner>
{
	[SerializeField] private Transform parent = null;
	[SerializeField] private Transform itemToSpawn = null;
	[SerializeField] private float spawnHeight = 1.0f;
	
	protected override void PostInit()
	{
		base.PostInit();
		Cursor.Instance.OnGroundHit += OnGroundHit;
	}

	private void OnGroundHit(Vector3 _groundPosition)
	{
		Instantiate(itemToSpawn, _groundPosition + Vector3.up * spawnHeight, Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)), parent);
	}
}
