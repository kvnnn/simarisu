using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameVector
{
	public static Vector2 MultiplyX(this Vector2 vector, int x)
	{
		vector.x = vector.x * x;
		return vector;
	}
}
