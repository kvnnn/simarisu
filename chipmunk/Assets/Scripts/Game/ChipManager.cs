using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChipManager : GameMonoBehaviour
{
	private ChipListParts chipListParts;
	private ChipSelectParts chipSelectParts;
	private ButtonParts startBattleButtonParts;

	private List<BaseChip> originalChipDeck = new List<BaseChip>();
	private List<BaseChip> currentChipDeck = new List<BaseChip>();

	public void Init()
	{
	}

	public List<BaseChip> GetSelectedChips()
	{
		List<BaseChip> chipList = new List<BaseChip>();

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

	private void ResetChipSelectParts()
	{
		ResetChipSelectFocus();
		chipSelectParts.ResetAllParts();
	}

	public void ResetChipSelectFocus()
	{
		chipSelectParts.ResetFocus();
	}

	public void FocusSelectParts(int index)
	{
		chipSelectParts.FocusTo(index);
	}

	private void UpdateChipParts()
	{
		chipListParts.SetChips(SelectChipsFromDeck());
	}

	private List<BaseChip> SelectChipsFromDeck()
	{
		// For test
		List<BaseChip> chips = new List<BaseChip>(){
			Chip.GetBaseChip(0),
			Chip.GetBaseChip(1),
			Chip.GetBaseChip(2),
			Chip.GetBaseChip(3),
			Chip.GetBaseChip(4),
		};
		return chips;
	}

	private void UpdateDeck()
	{

	}

	private void UpdateStartBattleButton()
	{
		startBattleButtonParts.isEnabled = chipSelectParts.isSetComplete;
	}

	private void ChipPartsClick(int chipIndex, BaseChip chip)
	{
		chipSelectParts.SetChipToFocusSelectParts(chipIndex, chip);
		UpdateStartBattleButton();
	}
}
