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

	public static void ScaleTo(this Transform transform, float xy)
	{
		Vector3 localScale = transform.localScale;
		localScale.x = xy;
		localScale.y = xy;
		transform.localScale = localScale;
	}
}
