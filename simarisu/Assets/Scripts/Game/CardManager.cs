using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CardManager : GameMonoBehaviour
{
	private CardListParts cardListParts;
	private ButtonParts startBattleButtonParts;

	private CardParts[] selectedCardParts = new CardParts[MAX_COUNT];
	private const int MAX_COUNT = 3;

	public System.Action<Card> cardPartsPushDown;
	public System.Action cardPartsPushUp;

	// private List<Card> originalCardDeck = new List<Card>();
	// private List<Card> currentCardDeck = new List<Card>();

	private bool isTouchLock = true;

	private readonly string[] SELECTED_CARD_ORDER_TEXTS = new string[]{"移動前", "移動中", "移動後"};

	public void Init(System.Action<Card> cardPartsPushDown, System.Action cardPartsPushUp)
	{
		this.cardPartsPushDown = cardPartsPushDown;
		this.cardPartsPushUp = cardPartsPushUp;
	}

	public List<Card> GetSelectedCards()
	{
		List<Card> cardList = new List<Card>();
		foreach (CardParts cardParts in selectedCardParts)
		{
			cardList.Add(cardParts.GetCard());
		}

		return cardList;
	}

	public void SetUIParts(CardListParts cardListParts, ButtonParts startBattleButtonParts)
	{
		this.cardListParts = cardListParts;
		cardListParts.cardPartsClick = CardPartsClick;
		cardListParts.cardPartsPushDown = cardPartsPushDown;
		cardListParts.cardPartsPushUp = cardPartsPushUp;

		this.startBattleButtonParts = startBattleButtonParts;

		UpdateParts();
	}

	public void UpdateParts()
	{
		selectedCardParts = new CardParts[MAX_COUNT];

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
		selectedCardParts[index] = cardParts;
		cardParts.Selected(SELECTED_CARD_ORDER_TEXTS[index]);
	}

	private void UnsetCard(CardParts cardParts)
	{
		cardParts.Deselected();
		selectedCardParts[Array.IndexOf(selectedCardParts, cardParts)] = null;
	}

	private int NextIndex()
	{
		int index = 0;
		foreach (CardParts card in selectedCardParts)
		{
			if (card == null) {break;}
			index++;
		}

		return index;
	}

	private bool IsAllCardSet()
	{
		return NextIndex() == MAX_COUNT;
	}

	public void EnableTouchEvent(bool enable)
	{
		isTouchLock = enable;
		EnableStartBattleButton(enable);
	}

#region Deck
	private List<Card> SelectCardsFromDeck()
	{
		// For Debug
		List<Card> cards = new List<Card>(){
			Card.GetCard(1),
			Card.GetCard(2),
			Card.GetCard(3),
			Card.GetCard(4),
			Card.GetCard(5),
			Card.GetCard(6),
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
		startBattleButtonParts.isEnabled = IsAllCardSet();
	}

	private void EnableStartBattleButton(bool enable)
	{
		startBattleButtonParts.isEnabled = enable;
	}
#endregion

#region Event
	private void CardPartsClick(CardParts parts)
	{
		if (!isTouchLock) {return;}

		if (parts.isSelected)
		{
			UnsetCard(parts);
		}
		else if (!IsAllCardSet())
		{
			SetCard(parts);
		}
		UpdateStartBattleButton();
	}
#endregion
}
