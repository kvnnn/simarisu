using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MonsterCharacter : BaseCharacter
{
	private Monster monster;

	public void Init(Monster monster)
	{
		this.monster = monster;
		base.Init(monster.hp, monster.damage);
	}

	public Chip SelectChip()
	{
		List<Chip> chips = monster.chips;

		// return null; //For Debug
		return chips[Random.Range(0, chips.Count - 1)];
	}
}
