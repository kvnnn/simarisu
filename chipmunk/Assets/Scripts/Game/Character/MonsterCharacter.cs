using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterCharacter : BaseCharacter
{
	private Monster monster;

	protected override Direction defaultDirection
	{
		get {return Direction.Left;}
	}

	public void Init(Monster monster)
	{
		this.monster = monster;
		base.Init();
	}
}
