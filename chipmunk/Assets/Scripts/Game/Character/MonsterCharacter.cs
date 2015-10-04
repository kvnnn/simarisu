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
		base.Init(monster.hp, monster.damage);
	}

	public Chip SelectChip()
	{
		List<Chip> chips = monster.chips;
		// return chips[1];
		return chips[Random.Range(0, chips.Count - 1)];
	}
}
