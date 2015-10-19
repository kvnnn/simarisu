using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	id 					: integer
	name 				: text
	description	: text
	sprite 			: text
	card				: text
	hp					: integer
	damage			: integer
	cure				: integer
	move				: integer
*/

public class Monster
{
#region Static
	public static Monster GetMonster(int id)
	{
		return GetMonster(id.ToString());
	}

	public static Monster GetMonster(string id)
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
	private string cardStr;
	public int hp {get; private set;}
	public int damage {get; private set;}
	public int cure {get; private set;}
	public int move {get; private set;}

	public Monster(DataRow rawData)
	{
		this.rawData = rawData;

		id = (int)rawData["id"];
		name = rawData["name"].ToString();
		description = rawData["description"].ToString();
		sprite = rawData["sprite"].ToString();
		cardStr = rawData["card"].ToString();
		hp = (int)rawData["hp"];
		damage = (int)rawData["damage"];
		cure = (int)rawData["cure"];
		move = (int)rawData["move"];
	}

	private List<Card> _card;
	public List<Card> card
	{
		get
		{
			if (_card == null)
			{
				_card = new List<Card>();
				string[] ids = cardStr.Split(',');
				foreach (string id in ids)
				{
					_card.Add(Card.GetCard(id));
				}
			}
			return _card;
		}
	}
#endregion
}
