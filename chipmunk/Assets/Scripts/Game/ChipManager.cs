using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChipManager : GameMonoBehaviour
{
	private ChipListParts chipListParts;
	private ChipSelectParts chipSelectParts;
	private ButtonParts startBattleButtonParts;

	private List<Chip> originalChipDeck = new List<Chip>();
	private List<Chip> currentChipDeck = new List<Chip>();

	public void Init()
	{
	}

	public List<Chip> GetSelectedChips()
	{
		List<Chip> chipList = new List<Chip>();

		foreach (int index in chipSelectParts.GetSelectedChipIndexList())
		{
			chipList.Add(chipListParts.GetChip(index));
		}

		return chipList;
	}

	public void SetUIParts(ChipListParts chipListParts, ChipSelectParts chipSelectParts, ButtonParts startBattleButtonParts)
	{
		this.chipListParts = chipListParts;
		chipListParts.chipPartsClick += ChipPartsClick;

		this.chipSelectParts = chipSelectParts;
		this.startBattleButtonParts = startBattleButtonParts;

		UpdateParts();
	}

	public void UpdateParts()
	{
		UpdateChipParts();
		ResetChipSelectParts();
		UpdateStartBattleButton();
	}

#region ChipSelectParts
	private void ResetChipSelectParts()
	{
		ResetChipSelectFocus();
		chipSelectParts.ResetAllParts();
	}

	public void ResetChipSelectFocus()
	{
		chipSelectParts.ResetFocus();
	}
#endregion

#region ChipParts
	public void FocusSelectParts(int index)
	{
		chipSelectParts.FocusTo(index);
	}

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
		startBattleButtonParts.isEnabled = chipSelectParts.isSetComplete;
	}
#endregion

#region Event
	private void ChipPartsClick(int chipIndex, Chip chip)
	{
		chipSelectParts.SetChipToFocusSelectParts(chipIndex, chip);
		UpdateStartBattleButton();
	}
#endregion
}
