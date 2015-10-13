using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardListParts : BaseUIParts
{
	[SerializeField]
	private List<CardParts> cardPartsLists;

	private CardParts pushDownCardParts;

	public System.Action<CardParts> cardPartsClick;
	public System.Action<Card> cardPartsPushDown;
	public System.Action cardPartsPushUp;

	public void SetCards(List<Card> cards)
	{
		RemoveAllCard();

		int index = 0;
		foreach (Card card in cards)
		{
			CardParts cardParts = cardPartsLists[index];
			cardParts.SetCard(card);

			index++;
		}

		UpdateAllCard();
	}

	public Card GetCard(int index)
	{
		return cardPartsLists[index].GetCard();
	}

	public void UpdateAllCard()
	{
		foreach (CardParts parts in cardPartsLists)
		{
			parts.UpdateParts();
		}
	}

	public void RemoveAllCard()
	{
		foreach (CardParts parts in cardPartsLists)
		{
			parts.RemoveCard();
		}
	}

#region Event
	public void CardClick(CardParts parts)
	{
		cardPartsClick(parts);
	}

	public void CardOnPushDown(CardParts parts)
	{
		pushDownCardParts = parts;
		cardPartsPushDown(parts.GetCard());
	}

	public void CardOnPushUp(CardParts parts)
	{
		if (pushDownCardParts == null) {return;}

		pushDownCardParts = null;
		cardPartsPushUp();
	}
#endregion
}
