using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardListParts : BaseUIParts
{
	[SerializeField]
	private List<CardParts> cardPartsLists;

	public System.Action<CardParts> cardPartsClick;

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
#endregion
}
