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
	[SerializeField] private NavPoint[] points;

	private Vector3 origin = Vector3.zero;
	
	private int XCount => (int) (size.x / gap);
	private int ZCount => (int) (size.z / gap);

	public bool GetRandomPoint(out Vector3 _point)
	{
		if (XCount == 0 || ZCount == 0)
		{
			_point = Vector3.zero;
			return false;
		}
		_point = points[Random.Range(0, points.Length)].GetPosition;
		return true;
	}
	
	public void GenerateGround()
	{
		RefreshOrigin();

		points = new NavPoint[ZCount * XCount];
		
		for (int z = 0; z < ZCount; ++z)
		{
			for (int x = 0; x < XCount; ++x)
			{
				Vector3 _position = origin + new Vector3(x * gap, size.y, z * gap);
				if (Physics.Raycast(_position, Vector3.down, out RaycastHit _hitInfo, size.y, groundLayer))
				{
					if (!Physics.CheckCapsule(_position + Vector3.down * minObstacleDistance, _position + Vector3.down * (size.y - minObstacleDistance), minObstacleDistance, obstacleLayer))
					{
						int _id = z * XCount + x;
						NavPoint _point = new NavPoint(_id, _hitInfo.point);
						points[_id] = _point;
						AddNeighborAt(_point, _id, x - 1, z);
						AddNeighborAt(_point, _id, x + 1, z);
						AddNeighborAt(_point, _id, x, z - 1);
						AddNeighborAt(_point, _id, x, z + 1);
					}
				}
			}
		}
	}

	private void AddNeighborAt(NavPoint _point, int _pointID, int _x, int _z)
	{
		int _neighborIndex = _z * XCount + _x;
		if (_neighborIndex < 0 || _neighborIndex >= points.Length) return;
		NavPoint _neighbor = points[_neighborIndex];
		if (_neighbor == null) return;
		_point.AddNeighbor(_neighborIndex);
		_neighbor.AddNeighbor(_pointID);
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

		if (points == null || points.Length == 0) return;
		for (int z = 0; z < ZCount; ++z)
		{
			for (int x = 0; x < XCount; ++x)
			{
				int _index = z * XCount + x;
				if (_index < 0 || _index >= points.Length) continue;
				NavPoint _point = points[_index];
				if (_point == null) continue;
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere(_point.GetPosition, 0.1f);
				List<int> _neighbors = _point.GetNeighbors;
				if (_neighbors == null) continue;
				int _count = _neighbors.Count;
				for (int i = 0; i < _count; ++i)
				{
					NavPoint _neighbor = points[_neighbors[i]];
					if (_neighbor == null) continue;
					Gizmos.color = Color.yellow;
					Gizmos.DrawLine(_point.GetPosition, _neighbor.GetPosition);
				}
			}
		}
	}
}
