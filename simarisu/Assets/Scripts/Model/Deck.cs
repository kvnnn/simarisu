using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
	id 					: integer
	cards 			: text
*/

public class Deck
{
#region Static
	private const int DECK_ID = 1;
	private static Deck deck;
	public static Deck GetDeck()
	{
		if (deck != null) {return deck;}

		string query = string.Format("select * from deck where id = {0}", DECK_ID);
		DataTable table = Database.instance.Execute(query);
		deck = new Deck(table.Rows[0]);
		return deck;
	}
#endregion

#region DeckData
	public DataRow rawData {get; private set;}
	public int id {get; private set;}
	private string cardsStr;

	public Deck(DataRow rawData)
	{
		this.rawData = rawData;

		id = (int)rawData["id"];
		cardsStr = rawData["cards"].ToString();
	}

	private List<Card> _cards;
	public List<Card> cards
	{
		get
		{
			if (_cards == null)
			{
				_cards = new List<Card>();
				string[] strArray = cardsStr.Split(',');
				foreach (string str in strArray)
				{
					_cards.Add(Card.GetCard(int.Parse(str)));
				}
			}

			return _cards;
		}
	}
#endregion
}
