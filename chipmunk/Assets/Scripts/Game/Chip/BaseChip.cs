using UnityEngine;
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
	public string position {get; private set;}
	public string range {get; private set;}

	public BaseChip(DataRow rawData)
	{
		this.rawData = rawData;
	}
}
