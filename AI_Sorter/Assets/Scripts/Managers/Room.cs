using UnityEngine;

public class Room : Singleton<Room>
{
	[SerializeField] private bool debug = false;
	[SerializeField] private Vector3 size = Vector3.one;

	public Vector3 GetOrigin => transform.position + Vector3.up * (size.y / 2.0f);

	public Vector3 GetRandomPoint()
	{
		return GetOrigin + new Vector3(Random.Range(-size.x / 2.0f, size.x / 2.0f), 0, Random.Range(-size.z / 2.0f, size.z / 2.0f));
	}

	private void OnDrawGizmos()
	{
		if (!debug) return;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(GetOrigin, size);
	}
}
