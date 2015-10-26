using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	id 					: integer
	name 				: text
	description	: text
	sprite 			: text
	cards				: text
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
	private string cardsStr;
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
		cardsStr = rawData["cards"].ToString();
		hp = (int)rawData["hp"];
		damage = (int)rawData["damage"];
		cure = (int)rawData["cure"];
		move = (int)rawData["move"];
	}

	private List<Card> _cards;
	public List<Card> cards
	{
		get
		{
			if (_cards == null)
			{
				_cards = new List<Card>();
				string[] ids = cardsStr.Split(',');
				foreach (string id in ids)
				{
					_cards.Add(Card.GetCard(id));
				}
			}
			return _cards;
		}
	}
#endregion
}
