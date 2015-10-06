using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CustomTransform
{
	public static void MoveTo(this Transform transform, Vector3 position)
	{
		transform.position = position;
	}

	public static void RotateY(this Transform transform, float y)
	{
		Vector3 angles = transform.eulerAngles;
		angles.y = y;
		transform.eulerAngles = angles;
	}
}
