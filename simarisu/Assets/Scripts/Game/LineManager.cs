using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineManager : GameMonoBehaviour
{
	private float leftDrawing = 0f;
	public List<Vector3> movePointList {get; private set;}

	private LineRenderer _line;
	private LineRenderer line
	{
		get
		{
			if (_line == null)
			{
				_line = gameObject.GetComponent<LineRenderer>();
			}
			return _line;
		}
	}

	public void Init()
	{
		line.sortingLayerName = "Line";
		ResetPoints(UserCharacter.DEFAULT_MAX_DRAWING);
		Hide();
	}

	public void Show()
	{
		line.enabled = true;
	}

	public void Hide()
	{
		line.enabled = false;
	}

	private void ResetPoints(float maxDrawing)
	{
		leftDrawing = maxDrawing;
		movePointList = new List<Vector3>();
		line.SetVertexCount(0);
	}

	public bool StartDrawing(Vector3 position, float maxDrawing)
	{
		ResetPoints(maxDrawing);

		Show();
		return AddPoint(position);
	}

	public bool AddPoint(Vector3 position)
	{
		if (movePointList.Count > 0)
		{
			float distance = Vector2.Distance(movePointList[movePointList.Count - 1], position);
			if (leftDrawing - distance < 0) {return false;}
			leftDrawing -= distance;
		}

		movePointList.Add(position);
		int index = movePointList.Count;

		line.SetVertexCount(index);
		line.SetPosition(index - 1, position);
		return true;
	}

	public bool EndDrawing(Vector3 position)
	{
		return AddPoint(position);
	}
}
