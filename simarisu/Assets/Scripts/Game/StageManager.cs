using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StageManager : GameMonoBehaviour
{
	private int stageId;
	private Transform stageTransform;
	private List<StageCell> cells;

	private const string STAGE_RESOURCE_PATH = "Prefabs/Stages/";

	private readonly Vector2 DEFAULT_USER_POSITION = new Vector2(5,2);

	public void Init()
	{
		DestoryIfExist();
		cells = null;
		stageTransform = null;
	}

	public void LoadStage(int stageId)
	{
		if (stageTransform != null && this.stageId == stageId) {return;}
		this.stageId = stageId;

		GameObject stageGameObject = Instantiate(Resources.Load<GameObject>(STAGE_RESOURCE_PATH + stageId));
		stageTransform = stageGameObject.transform;
		stageTransform.SetParent(transform);
		stageTransform.localPosition = Vector3.zero;
	}

#region Cell
	private List<StageCell> GetCells()
	{
		if (cells == null)
		{
			cells = new List<StageCell>();
			foreach (StageCell cell in gameObject.GetComponentsInChildren<StageCell>())
			{
				cells.Add(cell);
			}
		}

		return cells;
	}

	private StageCell GetCell(Vector2 position)
	{
		string positionStr = position.x + ":" + position.y;
		return GetCell(positionStr);
	}

	private StageCell GetCell(string position)
	{
		Transform cellTransform = stageTransform.Find(position);
		StageCell cell = cellTransform.gameObject.GetComponent<StageCell>();
		return cell;
	}

	public List<StageCell> GetAvailableCells(List<StageCell> usedCells)
	{
		List<StageCell> availableCells = new List<StageCell>();
		foreach (StageCell cell in GetCells())
		{
			if (cell.IsAvailable() && !usedCells.Contains(cell))
			{
				availableCells.Add(cell);
			}
		}

		return availableCells;
	}

	public StageCell GetDefaultUserCharacterCell()
	{
		return GetCell(DEFAULT_USER_POSITION);
	}
#endregion

	private void DestoryIfExist()
	{
		if (stageTransform != null)
		{
			Destroy(stageTransform.gameObject);
		}
	}
}
