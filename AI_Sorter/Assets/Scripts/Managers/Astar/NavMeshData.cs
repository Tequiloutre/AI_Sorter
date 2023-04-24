using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NavMeshData", menuName = "Custom/NavMeshData", order = 100)]
public class NavMeshData : ScriptableObject
{
	[SerializeField] private List<NavPoint> points;

	public List<NavPoint> GetPoints => points;
	public void SetPoints(List<NavPoint> _points) => points = _points;
}
