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
	private RectTransform cardTransform;
	[SerializeField]
	private Text text;
	[SerializeField]
	private Text selectedText;

	public bool isSelected {get; private set;}
	private const float SELECTED_POSITION_Y = 18.5f;
	private const float DESELECTED_POSITION_Y = 0f;

	public void SetCard(Card card)
	{
		this.card = card;
		Deselected();
	}

	public Card GetCard()
	{
		return card;
	}

	public void RemoveCard()
	{
		card = null;
	}

	public void Selected(int num)
	{
		isSelected = true;
		selectedText.text = num.ToString();
		cardTransform.MoveLocalY(SELECTED_POSITION_Y);
	}

	public void Deselected()
	{
		isSelected = false;
		selectedText.text = "";
		cardTransform.MoveLocalY(DESELECTED_POSITION_Y);
	}

	public void UpdateParts()
	{
		if (hasCard)
		{
			gameObject.SetActive(true);
			text.text = card.name;
		}
		else
		{
			gameObject.SetActive(false);
			text.text = "";
		}
	}
}
