using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardParts : BaseUIParts
{
	private Card card;
	public bool hasCard
	{
		get {return card != null;}
	}

	[SerializeField]
	private Text text;

	public void SetCard(Card card)
	{
		this.card = card;
	}

	public Card GetCard()
	{
		return card;
	}

	public void RemoveCard()
	{
		card = null;
	}

	public void UpdateParts()
	{
		if (hasCard) {
			gameObject.SetActive(true);
			text.text = card.name;
		} else {
			gameObject.SetActive(false);
			text.text = "";
		}
	}
}
