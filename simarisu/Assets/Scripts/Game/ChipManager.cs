using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChipManager : GameMonoBehaviour
{
	private ChipListParts chipListParts;
	private ButtonParts startBattleButtonParts;

	private List<Chip> originalChipDeck = new List<Chip>();
	private List<Chip> currentChipDeck = new List<Chip>();

	public void Init()
	{
	}

	public List<Chip> GetSelectedChips()
	{
		List<Chip> chipList = new List<Chip>();

		return chipList;
	}

	public void SetUIParts(ChipListParts chipListParts, ButtonParts startBattleButtonParts)
	{
		this.chipListParts = chipListParts;
		chipListParts.chipPartsClick += ChipPartsClick;

		this.startBattleButtonParts = startBattleButtonParts;

		UpdateParts();
	}

	public void UpdateParts()
	{
		UpdateChipParts();
		UpdateStartBattleButton();
	}

#region ChipParts
	private void UpdateChipParts()
	{
		chipListParts.SetChips(SelectChipsFromDeck());
	}
#endregion

#region Deck
	private List<Chip> SelectChipsFromDeck()
	{
		// For Debug
		List<Chip> chips = new List<Chip>(){
			Chip.GetChip(0),
			Chip.GetChip(1),
			Chip.GetChip(2),
			Chip.GetChip(3),
			Chip.GetChip(4),
		};
		return chips;
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
	private void ChipPartsClick(int chipIndex, Chip chip)
	{
		UpdateStartBattleButton();
	}
#endregion
}
