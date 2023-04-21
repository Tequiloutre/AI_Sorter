using System.Collections.Generic;
using UnityEngine;

public class NavMesh : Singleton<NavMesh>
{
	[SerializeField] private bool debug = false;
	[SerializeField] private Vector3 size = Vector3.one;
	[SerializeField] private float gap = 1.0f,
		minObstacleDistance = 0.1f;
	[SerializeField] private LayerMask groundLayer = 0,
		obstacleLayer = 0;

	private Vector3 origin = Vector3.zero;
	[HideInInspector, SerializeField]
	private List<Vector3> points = new List<Vector3>();

	public bool GetRandomPoint(out Vector3 _point)
	{
		if (points.Count == 0)
		{
			_point = Vector3.zero;
			return false;
		}
		_point = points[Random.Range(0, points.Count)];
		return true;
	}
	
	public void GenerateGround()
	{
		RefreshOrigin();
		
		points.Clear();

		Vector3 _max = origin + new Vector3(size.x, size.y, size.z);
		
		float _z = origin.z;
		while (_z <= _max.z)
		{
			float _x = origin.x;
			while (_x <= _max.x)
			{
				if (Physics.Raycast(new Vector3(_x, _max.y, _z), Vector3.down, out RaycastHit _hitInfo, size.y, groundLayer))
				{
					if (!Physics.SphereCast(new Vector3(_x, _max.y, _z), minObstacleDistance, Vector3.down, out RaycastHit _hitInfoObstacle, size.y, obstacleLayer))
						points.Add(_hitInfo.point);
				}
				_x += gap;
			}
			_z += gap;
		}
	}

	private void RefreshOrigin()
	{
		origin = transform.position - new Vector3(size.x / 2.0f, 0.0f, size.z / 2.0f);
	}

	private void OnDrawGizmos()
	{
		if (!debug) return;

		Transform _transform = transform;
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(_transform.position + Vector3.up * (size.y / 2.0f), size);

		RefreshOrigin();
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(origin, 0.1f);

		int _count = points.Count;
		for (int i = 0; i < _count; ++i)
			Gizmos.DrawWireSphere(points[i], 0.1f);
	}
}
