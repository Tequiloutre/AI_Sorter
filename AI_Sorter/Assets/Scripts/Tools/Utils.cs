using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
	public static Vector3 ResetY(this Vector3 _vector)
	{
		return new Vector3(_vector.x, 0.0f, _vector.z);
	}
}
