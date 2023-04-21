using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NavPoint
{
	[SerializeField] private Vector3 position = Vector3.zero;
	[SerializeField, HideInInspector] private List<NavPoint> neighbors = new List<NavPoint>();

	public Vector3 GetPosition => position;

	public NavPoint(Vector3 _position)
	{
		position = _position;
	}

	public void AddNeighbor(NavPoint _neighbor, bool _addBack = false)
	{
		if (neighbors.Contains(_neighbor)) return;
		neighbors.Add(_neighbor);
		_neighbor.AddNeighbor(this);
	}
}
