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

	public Card SelectCard()
	{
		List<Card> cards = monster.card;

		// return null; //For Debug
		return cards[Random.Range(0, cards.Count - 1)];
	}
}
