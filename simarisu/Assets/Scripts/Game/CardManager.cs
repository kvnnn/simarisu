using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CardManager : GameMonoBehaviour
{
	private CardListParts cardListParts;
	private ButtonParts startBattleButtonParts;

	private CardParts[] selectedCardIndex = new CardParts[MAX_COUNT];
	private const int MAX_COUNT = 3;

	private List<Card> originalCardDeck = new List<Card>();
	private List<Card> currentCardDeck = new List<Card>();

	public void Init()
	{
	}

	public List<Card> GetSelectedCards()
	{
		List<Card> cardList = new List<Card>();

		return cardList;
	}

	public void SetUIParts(CardListParts cardListParts, ButtonParts startBattleButtonParts)
	{
		this.cardListParts = cardListParts;
		cardListParts.cardPartsClick += CardPartsClick;

		this.startBattleButtonParts = startBattleButtonParts;

		UpdateParts();
	}

	public void UpdateParts()
	{
		selectedCardIndex = new CardParts[MAX_COUNT];

		UpdateCardParts();
		UpdateStartBattleButton();
	}

	private void UpdateCardParts()
	{
		cardListParts.SetCards(SelectCardsFromDeck());
	}

	private void SetCard(CardParts cardParts)
	{
		int index = NextIndex();
		selectedCardIndex[index] = cardParts;
		cardParts.Selected(index);
	}

	private void UnsetCard(CardParts cardParts)
	{
		cardParts.Deselected();
		selectedCardIndex[Array.IndexOf(selectedCardIndex, cardParts)] = null;
	}

	private int NextIndex()
	{
		int index = 0;
		foreach (CardParts card in selectedCardIndex)
		{
			if (card == null) {break;}
			index++;
		}

		return index;
	}

	private bool IsCardSet()
	{
		return NextIndex() == MAX_COUNT;
	}

#region Deck
	private List<Card> SelectCardsFromDeck()
	{
		// For Debug
		List<Card> cards = new List<Card>(){
			Card.GetCard(0),
			Card.GetCard(1),
			Card.GetCard(2),
			Card.GetCard(3),
			Card.GetCard(4),
			Card.GetCard(4),
		};
		return cards;
	}

	private void UpdateDeck()
	{

	}
#endregion

#region Button
	private void UpdateStartBattleButton()
	{
		startBattleButtonParts.isEnabled = false;
	}
#endregion

#region Event
	private void CardPartsClick(CardParts parts)
	{
		if (parts.isSelected)
		{
			UnsetCard(parts);
		}
		else if (!IsCardSet())
		{
			SetCard(parts);
			UpdateStartBattleButton();
		}
	}
#endregion
}
