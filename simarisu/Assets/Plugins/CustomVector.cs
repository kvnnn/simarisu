using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class CustomVector
{
	public static Vector2 MultiplyX(this Vector2 vector, int x)
	{
		vector.x = vector.x * x;
		return vector;
	}

	public static Vector2 GetFromString(string str)
	{
		string[] strArray = str.Split(':');
		return new Vector2(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]));
	}
}
