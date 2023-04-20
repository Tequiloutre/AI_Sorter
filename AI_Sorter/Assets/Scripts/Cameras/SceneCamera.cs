using System;
using UnityEngine;

public class SceneCamera : MonoBehaviour
{
	[SerializeField] private Transform target = null;
	[SerializeField] private float minDistance = 3.0f,
		maxDistance = 20.0f,
		horizontalAngle = -180.0f,
		verticalAngle = 60.0f;

	private float distance = 20.0f;

	private void Start()
	{
		InputManager.Instance.OnInputMouseWheel += Zoom;
	}

	private void Zoom(float _value)
	{
		distance = Mathf.Clamp(distance - _value, minDistance, maxDistance);
	}

	private void LateUpdate()
	{
		MoveToTarget();
	}

	private void MoveToTarget()
	{
		Vector3 _direction = Quaternion.Euler(verticalAngle, horizontalAngle, 0.0f) * Vector3.forward;
		transform.position = target.position - _direction.normalized * distance;
	}
}
