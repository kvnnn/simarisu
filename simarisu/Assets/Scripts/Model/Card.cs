using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	id 							: integer
	name 						: text
	description			: text
	sprite 					: text
	effect 					: text
	rarity 					: integer
		S = 4,
		A = 3,
		B = 2,
		C = 1,
	type 						: integer
		Attack = 1,
		Cure = 2,
		Equipment = 3,
		Support = 4,
		Other = 5,
	specialEffect		: integer
		Null = 0,
		AttackUp = 1,
		MoveUp = 2,
		HpUp = 3,
	damage 					: integer
	range 					: string
*/

public class Card
{
#region Static
	public static Card GetCard(int id)
	{
		return GetCard(id.ToString());
	}

	public static Card GetCard(string id)
	{
		string query = string.Format("select * from card where id = {0}", id);
		DataTable table = Database.instance.Execute(query);
		return new Card(table.Rows[0]);
	}
#endregion

#region CardData
	public DataRow rawData {get; private set;}
	public int id {get; private set;}
	public string name {get; private set;}
	public string description {get; private set;}
	public string sprite {get; private set;}
	public string effect {get; private set;}

	public Rarity rarity {get; private set;}
	public enum Rarity
	{
		S = 4,
		A = 3,
		B = 2,
		C = 1,
	}
	public Type type {get; private set;}
	public enum Type
	{
		Attack = 1,
		Cure = 2,
		Equipment = 3,
		Support = 4,
		Other = 5,
	}
	public SpecialEffect specialEffect {get; private set;}
	public enum SpecialEffect
	{
		Null = 0,
		AttackUp = 1,
		MoveUp = 2,
		HpUp = 3,
	}

	public int damage {get; private set;}

	private string rangeStr;

	public Card(DataRow rawData)
	{
		this.rawData = rawData;

		id = (int)rawData["id"];
		name = rawData["name"].ToString();
		description = rawData["description"].ToString();
		sprite = rawData["sprite"].ToString();
		effect = rawData["effect"].ToString();
		rarity = (Rarity)rawData["rarity"];

		type = (Type)rawData["type"];
		specialEffect = (SpecialEffect)rawData["specialEffect"];

		damage = (int)rawData["damage"];
		rangeStr = rawData["range"].ToString();
	}

	private List<Vector2> _ranges;
	public List<Vector2> ranges
	{
		get
		{
			if (_ranges == null)
			{
				_ranges = new List<Vector2>();
				string[] strArray = rangeStr.Split(',');
				foreach (string str in strArray)
				{
					_ranges.Add(CustomVector.GetFromString(str));
				}
			}
			return _ranges;
		}
	}
#endregion
}
