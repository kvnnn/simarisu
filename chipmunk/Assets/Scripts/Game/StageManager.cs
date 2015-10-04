using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : GameMonoBehaviour
{
	public const int MAX_X = 5;
	public const int MIN_X = 0;
	public const int MAX_Y = 2;
	public const int MIN_Y = 0;

	public void Init()
	{

	}

	public Vector3 GetCellPosition(int x, int y)
	{
		Transform cellTransform = transform.Find(string.Format("{0},{1}", x, y));
		return cellTransform.position;
	}

	public Vector3 GetCellPosition(Vector2 position)
	{
		return GetCellPosition((int)position.x, (int)position.y);
	}

	public bool HasCell(int x, int y)
	{
		return x >= MIN_X && x <= MAX_X && y >= MIN_Y && y <= MAX_Y;
	}

	public bool HasCell(Vector2 position)
	{
		return HasCell((int)position.x, (int)position.y);
	}
}
