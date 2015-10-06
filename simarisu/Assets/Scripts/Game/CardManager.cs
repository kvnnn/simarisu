using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardManager : GameMonoBehaviour
{
	private CardListParts cardListParts;
	private ButtonParts startBattleButtonParts;

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
		UpdateCardParts();
		UpdateStartBattleButton();
	}

#region CardParts
	private void UpdateCardParts()
	{
		cardListParts.SetCards(SelectCardsFromDeck());
	}
#endregion

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
	private void CardPartsClick(int cardIndex, Card card)
	{
		UpdateStartBattleButton();
	}
#endregion
}
