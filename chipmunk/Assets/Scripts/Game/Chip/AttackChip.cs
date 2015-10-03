using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackChip : BaseChip
{
	// damage depends on user character power
	public virtual int damage
	{
		get {return 5;}
	}
}
