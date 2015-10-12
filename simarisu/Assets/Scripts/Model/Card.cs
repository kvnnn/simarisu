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
	type 				: integer (1:Attack, 2:Support, 3:Other)
	damage 			: integer
	range_type 	: integer
	range_size	: real
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
		Support = 2,
		Other = 3,
	}
	public int damage {get; private set;}

	public int rangeType {get; private set;}
	public double rangeSize {get; private set;}

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

		rangeType = (int)rawData["range_type"];
		rangeSize = (double)rawData["range_size"];
	}
#endregion
}
