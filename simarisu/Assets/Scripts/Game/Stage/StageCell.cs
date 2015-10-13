using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StageCell : GameMonoBehaviour
{
	[SerializeField]
	private bool isAvailable = true;
	private Vector2? position = null;

	public Vector2 PositionInWorld()
	{
		return transform.position;
	}

	public Vector2 Position()
	{
		if (position == null)
		{
			position = CustomVector.GetFromString(gameObject.name);
		}
		return (Vector2)position;
	}

	public bool IsAvailable()
	{
		return isAvailable;
	}
}
