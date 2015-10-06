using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	id 					: integer
	name 				: text
	description	: text
	sprite 			: text
	effect 			: text
	rarity 			: integer (1:C, 2:B, 3:A, 4:S)
	type 				: integer (1:Move, 2:Attack, 3:Cure, 4:Other)
	damage 			: integer
	position 		: text
	range 			: text
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
		Move = 1,
		Attack = 2,
		Cure = 3,
		Other = 4,
	}
	public virtual int damage {get; private set;}
	private string positionStr;
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
		damage = (int)rawData["damage"];
		positionStr = rawData["position"].ToString();
		rangeStr = rawData["range"].ToString();
	}

	private Vector2? _position = null;
	public Vector2 position
	{
		get
		{
			if (_position == null)
			{
				string[] strArray = positionStr.Split(':');
				_position = new Vector2(float.Parse(strArray[0]), float.Parse(strArray[1]));
			}
			return (Vector2)_position;
		}
	}

	private Vector2? _range = null;
	public Vector2 range
	{
		get
		{
			if (_range == null)
			{
				string[] strArray = rangeStr.Split(':');
				_range = new Vector2(float.Parse(strArray[0]), float.Parse(strArray[1]));
			}
			return (Vector2)_range;
		}
	}
#endregion
}
