using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineManager : GameMonoBehaviour
{
	private float leftDrawing = 0f;
	private List<Vector3> pointList = new List<Vector3>();

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
		pointList = new List<Vector3>();
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
		if (pointList.Count > 0)
		{
			float distance = Vector2.Distance(pointList[pointList.Count - 1], position);
			UnityEngine.Debug.LogError(leftDrawing - distance);
			if (leftDrawing - distance < 0) {return false;}
			leftDrawing -= distance;
		}

		pointList.Add(position);
		int index = pointList.Count;

		line.SetVertexCount(index);
		line.SetPosition(index - 1, position);
		return true;
	}

	public bool EndDrawing(Vector3 position)
	{
		return AddPoint(position);
	}
}
