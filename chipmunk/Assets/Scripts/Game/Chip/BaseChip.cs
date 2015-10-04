using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BaseChip
{
	public DataRow rawData {get; private set;}

	public int id {get; private set;}
	public string chipName {get; private set;}
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
	public string rangeStr;

	public BaseChip(DataRow rawData)
	{
		this.rawData = rawData;

		id = (int)rawData["id"];
		chipName = rawData["name"].ToString();
		description = rawData["description"].ToString();
		sprite = rawData["sprite"].ToString();
		effect = rawData["effect"].ToString();
		rarity = (Rarity)rawData["rarity"];
		type = (Type)rawData["type"];
		positionStr = rawData["position"].ToString();
		rangeStr = rawData["range"].ToString();

		string damageStr = rawData["damage"].ToString();
		int tryToParse = 0;
		Int32.TryParse(damageStr, out tryToParse);
		damage = tryToParse;
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
}
