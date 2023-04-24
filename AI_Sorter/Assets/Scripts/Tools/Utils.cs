using UnityEngine;

public static class Utils
{
	public static Vector3 ResetY(this Vector3 _vector)
	{
		return new Vector3(_vector.x, 0.0f, _vector.z);
	}

	public static Vector3 Offset(this Vector3 _position, Vector3 _offset, Transform _transform)
	{
		return _position
			+ _transform.right * _offset.x
			+ _transform.up * _offset.y
			+ _transform.forward * _offset.z;
	}
}
