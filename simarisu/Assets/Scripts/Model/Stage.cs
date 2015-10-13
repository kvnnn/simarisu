using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	id 					: integer
	range 			: text
	monsters		: text
	rate 				: real
	stage_id		: integer
*/

public class Stage
{
#region Static
	public static Stage GetStage(int id)
	{
		return GetStage(id.ToString());
	}

	public static Stage GetStage(string id)
	{
		string query = string.Format("select * from stage where id = {0}", id);
		DataTable table = Database.instance.Execute(query);
		return new Stage(table.Rows[0]);
	}

	private static List<Stage> stageList;
	public static List<Stage> GetAllStage()
	{
		if (stageList != null) {return stageList;}

		stageList = new List<Stage>();
		string query = string.Format("select * from stage");
		DataTable table = Database.instance.Execute(query);
		foreach (DataRow data in table.Rows)
		{
			stageList.Add(new Stage(data));
		}
		return stageList;
	}
#endregion

#region StageData
	public DataRow rawData {get; private set;}
	public int id {get; private set;}
	public int stageId {get; private set;}
	public double rate {get; private set;}
	private string monstersStr;
	private string rangeStr;
	public int minRange {get; private set;}
	public int maxRange {get; private set;}

	public Stage(DataRow rawData)
	{
		this.rawData = rawData;

		id = (int)rawData["id"];
		stageId = (int)rawData["stage_id"];
		rate = (double)rawData["rate"];
		monstersStr = rawData["monsters"].ToString();

		rangeStr = rawData["range"].ToString();
		string[] ranges = rangeStr.Split(':');
		minRange = int.Parse(ranges[0]);
		maxRange = int.Parse(ranges[1]);
	}

	private List<Monster> _monsters;
	public List<Monster> monsters
	{
		get
		{
			if (_monsters == null)
			{
				_monsters = new List<Monster>();
				string[] ids = monstersStr.Split(',');
				foreach (string id in ids)
				{
					_monsters.Add(Monster.GetMonster(id));
				}
			}
			return _monsters;
		}
	}
#endregion
}
