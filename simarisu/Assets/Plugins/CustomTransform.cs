using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CustomTransform
{
	public static void MoveLocalY(this RectTransform transform, float y)
	{
		Vector3 position = transform.anchoredPosition;
		position.y = y;
		transform.anchoredPosition = position;
	}
}
