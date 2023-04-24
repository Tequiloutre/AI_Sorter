using System;
using System.Collections.Generic;
using UnityEngine;

public class SightComponent : Component
{
	public event Action<Transform> OnTargetDetected = null;
	public event Action<Transform> OnTargetLost = null; 

	[SerializeField] private Transform origin = null;
	[SerializeField] private Vector3 offset = Vector3.zero;
	[SerializeField] private float range = 5.0f,
		angle = 90.0f;
	[SerializeField] private LayerMask detectionlayer = 0;
	[SerializeField] private List<Collider> targets = new List<Collider>();

	private bool targetDetected = false;

	private Vector3 GetOriginPosition => origin.position.Offset(offset, origin);

	public void SearchTarget()
	{
		targets.Clear();
		Vector3 _originPosition = GetOriginPosition;
		Collider[] _hits = Physics.OverlapSphere(_originPosition, range, detectionlayer);

		List<Collider> _targets = new List<Collider>();
		int _hitCount = _hits.Length;
		for (int i = 0; i < _hitCount; ++i)
		{
			Collider _hit = _hits[i];
			if (_hit.transform == entity.transform || targets.Contains(_hit)) continue;
			Vector3 _direction = (_hit.transform.position - _originPosition).ResetY().normalized;
			if (Vector3.Dot(_direction, origin.forward) < Mathf.Cos(angle / 2.0f * Mathf.Deg2Rad)) continue;
			_targets.Add(_hit);
			targets.Add(_hit);
			OnTargetDetected?.Invoke(_hit.transform);
		}

		int _targetCount = targets.Count;
		for (int i = 0; i < _targetCount; ++i)
		{
			Collider _target = targets[i];
			if (_targets.Contains(_target)) continue;
			targets.Remove(_target);
			OnTargetLost?.Invoke(_target.transform);
		}
		
		targetDetected = targets.Count > 0;
	}

	protected override void DrawDebug()
	{
		base.DrawDebug();
		if (!origin) return;
		
		Gizmos.color = targetDetected ? Color.green : Color.red;
		Vector3 _originPosition = GetOriginPosition;
		Gizmos.DrawWireSphere(GetOriginPosition, range);
		Vector3 _left = _originPosition + Quaternion.Euler(0, -angle / 2.0f, 0) * origin.forward * range;
		Vector3 _right = _originPosition + Quaternion.Euler(0, angle / 2.0f, 0) * origin.forward * range;
		Gizmos.DrawLine(_originPosition, _left);
		Gizmos.DrawLine(_originPosition, _right);
	}
}
