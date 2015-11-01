using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StageManager : GameMonoBehaviour
{
	private Stage currentStage
	{
		get {return GetCurrentStage();}
	}
	private int stageCount;
	private int stageIndex;

	private int stageId;
	private Transform stageTransform;
	private List<StageCell> cells;

	private List<StageCell> route;
	public int routeCount
	{
		get {return route.Count - 1;}
	}

	public System.Action<StageCell> onCellPointerEnter;

	private const int MAX_MONSTER = 3;
	private const string STAGE_RESOURCE_PATH = "Prefabs/Stages/";
	private readonly Vector2 DEFAULT_USER_POSITION = new Vector2(5,2);

	public void Init(System.Action<StageCell> onCellPointerEnter)
	{
		DestoryIfExist();

		this.onCellPointerEnter = onCellPointerEnter;
		cells = null;
		stageTransform = null;
	}

	public void LoadStage()
	{
		if (stageTransform != null && this.stageId == currentStage.stageId) {return;}
		this.stageId = currentStage.stageId;

		GameObject stageGameObject = Instantiate(Resources.Load<GameObject>(STAGE_RESOURCE_PATH + stageId));
		stageTransform = stageGameObject.transform;
		stageTransform.SetParent(transform);
		stageTransform.localPosition = Vector3.zero;

		foreach (StageCell cell in GetCells())
		{
			cell.onPointerEnter = onCellPointerEnter;
		}
	}

#region Route
	public List<StageCell> GetRoute()
	{
		route.RemoveAt(0);
		return route;
	}

	public void CreateRoute(StageCell startCell)
	{
		route = new List<StageCell>();
		route.Add(startCell);
	}

	public void AddRoute(StageCell cell)
	{
		if (IsUsedForRoute(cell))
		{
			RemoveLastRouteIfCan(cell);
		}
		else
		{
			StageCell lastCell = route.Last();
			Vector2 diffPosition = cell.Position() - lastCell.Position();
			if (Mathf.Abs(diffPosition.x) <= 1 && Mathf.Abs(diffPosition.y) <= 1)
			{
				route.Add(cell);
				cell.SetSelectColor();

				if (routeCount != 1)
				{
					lastCell.SetArrow(diffPosition);
				}
			}
		}
	}

	public void RemoveLastRouteIfCan(StageCell cell)
	{
		StageCell lastCell = route.Last();
		if (lastCell != cell) {return;}

		route.Remove(cell);
		cell.UnsetSelectColor();
	}

	private bool IsUsedForRoute(StageCell cell)
	{
		return route.Contains(cell);
	}
#endregion

#region Cell
	private List<StageCell> GetCells()
	{
		if (cells == null)
		{
			cells = new List<StageCell>();
			foreach (StageCell cell in gameObject.GetComponentsInChildren<StageCell>(true))
			{
				cells.Add(cell);
			}
		}

		return cells;
	}

	public StageCell GetCell(Vector2 position)
	{
		string positionStr = position.x + ":" + position.y;
		return GetCell(positionStr);
	}

	private StageCell GetCell(string position)
	{
		Transform cellTransform = stageTransform.Find(position);
		StageCell cell = null;
		if (cellTransform != null)
		{
			cell = cellTransform.gameObject.GetComponent<StageCell>();
		}
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

	public void ResetAllCellColor()
	{
		foreach (StageCell cell in GetCells())
		{
			cell.UnsetSelectColor();
		}
	}

	public void ResetAllRangeCellColor()
	{
		foreach (StageCell cell in GetCells())
		{
			cell.UnsetRangeColor();
		}
	}

	public StageCell GetDefaultUserCharacterCell()
	{
		return GetCell(DEFAULT_USER_POSITION);
	}

	private void DestoryIfExist()
	{
		if (stageTransform != null)
		{
			Destroy(stageTransform.gameObject);
		}
	}
#endregion

#region StageData
	public void ResetStatus()
	{
		stageCount = 1;
		stageIndex = 0;
	}

	public void NextStage()
	{
		stageCount++;
	}

	private Stage CheckAndGetCurrentStage()
	{
		CheckStage();
		return GetCurrentStage();
	}

	private Stage GetCurrentStage()
	{
		return Stage.GetAllStage()[stageIndex];
	}

	private void CheckStage()
	{
		Stage stageData = GetCurrentStage();
		bool isProperStage = stageData.maxRange >= stageCount && stageCount >= stageData.minRange;

		if (!isProperStage)
		{
			stageIndex++;
			CheckStage();
		}
	}

	public List<Monster> PickMonster()
	{
		List<Monster> monsterList = currentStage.monsters;
		int numSelect = Random.Range(1, Mathf.Min(monsterList.Count, MAX_MONSTER));

		System.Random random = new System.Random();
		return monsterList.OrderBy(x => random.Next()).Take(numSelect).ToList();
	}
#endregion
}
