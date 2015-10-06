using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineManager : GameMonoBehaviour
{
	private int index = 0;

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
		index = 0;
		line.sortingLayerName = "Line";
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

	public void StartDrawing(Vector3 position)
	{
		index = 0;
		line.SetVertexCount(0);
		Show();
		AddPoint(position);
	}

	public void AddPoint(Vector3 position)
	{
		index++;
		line.SetVertexCount(index + 1);
		line.SetPosition(index, position);
	}

	public void EndDrawing(Vector3 position)
	{
		AddPoint(position);
	}
}
