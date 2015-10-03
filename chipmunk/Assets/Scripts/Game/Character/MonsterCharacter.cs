using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterCharacter : BaseCharacter
{
	protected override Direction defaultDirection
	{
		get {return Direction.Left;}
	}
}
