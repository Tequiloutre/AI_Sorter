using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NavPoint
{
	[SerializeField] private int id = 0;
	[SerializeField] private Vector3 position = Vector3.zero;
	[SerializeField] private List<int> neighbors = new List<int>();

	public int GetID => id;
	public Vector3 GetPosition => position;
	public List<int> GetNeighbors => neighbors;

	public NavPoint(int _id, Vector3 _position)
	{
		id = _id;
		position = _position;
	}

	public void AddNeighbor(int _neighborID)
	{
		if (neighbors.Contains(_neighborID)) return;
		neighbors.Add(_neighborID);
	}
}
