using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : GameMonoBehaviour
{
	public void Init()
	{

	}

	public Vector3 GetCellPosition(int x, int y)
	{
		Transform cellTransform = transform.Find(string.Format("{0},{1}", x, y));
		return cellTransform.position;
	}
}
