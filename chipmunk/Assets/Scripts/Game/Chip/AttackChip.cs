using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackChip : BaseChip
{
	// damage depends on user character power
	public override int damage
	{
		get {return 5;}
	}

	public AttackChip(DataRow rawData) : base(rawData)
	{
	}
}
