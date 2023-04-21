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
	[HideInInspector, SerializeField] private List<NavPoint> points;
	
	private int XCount => (int) (size.x / gap);
	private int ZCount => (int) (size.z / gap);

	public bool GetRandomPoint(out Vector3 _point)
	{
		if (XCount == 0 || ZCount == 0)
		{
			_point = Vector3.zero;
			return false;
		}
		_point = points[Random.Range(0, points.Count)].GetPosition;
		return true;
	}
	
	public void GenerateGround()
	{
		RefreshOrigin();
		
		points.Clear();
		
		for (int z = 0; z < ZCount; ++z)
		{
			for (int x = 0; x < XCount; ++x)
			{
				Vector3 _position = origin + new Vector3(x * gap, size.y, z * gap);
				if (Physics.Raycast(_position, Vector3.down, out RaycastHit _hitInfo, size.y, groundLayer))
				{
					if (!Physics.SphereCast(_position, minObstacleDistance, Vector3.down, out RaycastHit _hitInfoObstacle, size.y, obstacleLayer))
					{
						NavPoint _point = new NavPoint(_hitInfo.point);
						AddNeighborAt(_point, x - 1, z);
						AddNeighborAt(_point, x + 1, z);
						AddNeighborAt(_point, x, z - 1);
						AddNeighborAt(_point, x, z + 1);
						points.Add(_point);
					}
				}
			}
		}
	}

	private void AddNeighborAt(NavPoint _point, int _x, int _z)
	{
		int _neighborIndex = _z * XCount + _x;
		if (_neighborIndex < 0 || _neighborIndex >= points.Count) return;
		NavPoint _neighbor = points[_z * XCount + _x];
		if (_neighbor == null) return;
		_point.AddNeighbor(_neighbor);
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

		if (points.Count == 0) return;
		for (int z = 0; z < ZCount; ++z)
		{
			for (int x = 0; x < XCount; ++x)
			{
				int _index = z * XCount + x;
				if (_index < 0 || _index >= points.Count) continue;
				NavPoint _point = points[_index];
				if (_point == null) continue;
				Gizmos.DrawWireSphere(_point.GetPosition, 0.1f);
			}
		}
	}
}
