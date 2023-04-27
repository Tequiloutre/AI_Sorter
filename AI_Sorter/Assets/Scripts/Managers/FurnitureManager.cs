using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : Singleton<FurnitureManager>
{
	[SerializeField] private List<Storage> storages = null;

	public List<Storage> GetStorages => storages;
	public Storage GetNearestStorage(Vector3 _position)
	{
		int _count = storages.Count;
		if (_count == 0) return null;
		Storage _nearestStorage = storages[0];
		float _shortestDistance = Vector3.Distance(_position, _nearestStorage.transform.position);
		for (int i = 1; i < _count; ++i)
		{
			Storage _storage = storages[i];
			float _distance = Vector3.Distance(_position, _storage.transform.position);
			if (_distance >= _shortestDistance) continue;
			_nearestStorage = _storage;
			_shortestDistance = _distance;
		}
		return _nearestStorage;
	}
}
