using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	id 					: integer
	name 				: text
	description	: text
	sprite 			: text
	chips				: text
	hp					: integer
*/

public class Monster
{
#region Static
	public static Monster GetMonster(int id)
	{
		string query = string.Format("select * from monster where id = {0}", id);
		DataTable table = Database.instance.Execute(query);
		return new Monster(table.Rows[0]);
	}
#endregion

#region MonsterData
	public DataRow rawData {get; private set;}
	public int id {get; private set;}
	public string name {get; private set;}
	public string description {get; private set;}
	public string sprite {get; private set;}
	private string chipsStr;
	public int hp {get; private set;}

	public Monster(DataRow rawData)
	{
		this.rawData = rawData;

		id = (int)rawData["id"];
		name = rawData["name"].ToString();
		description = rawData["description"].ToString();
		sprite = rawData["sprite"].ToString();
		chipsStr = rawData["chips"].ToString();
		hp = (int)rawData["hp"];
	}
#endregion
}
