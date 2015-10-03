using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameTransform
{
	public static void RotateY(this Transform transform, float y)
	{
		Vector3 angles = transform.eulerAngles;
		angles.y = y;
		transform.eulerAngles = angles;
	}
}
