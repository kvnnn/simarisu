using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MonsterCharacter : BaseCharacter
{
	private Monster monster;
	public System.Action<MonsterCharacter> onDead {private get; set;}

	public void Init(Monster data, int order)
	{
		this.monster = data;
		base.Init(monster.hp, monster.damage, monster.cure, order);
	}

	public Card SelectCard()
	{
		List<Card> cards = monster.card;

		// return null; //For Debug
		return cards[Random.Range(0, cards.Count - 1)];
	}

	protected override void OnDead()
	{
		onDead(this);
	}
}
