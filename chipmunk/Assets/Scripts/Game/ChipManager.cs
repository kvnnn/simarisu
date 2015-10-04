using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChipManager : GameMonoBehaviour
{
	private ChipListParts chipListParts;
	private ChipSelectParts chipSelectParts;

	private List<BaseChip> originalChipDeck = new List<BaseChip>();
	private List<BaseChip> currentChipDeck = new List<BaseChip>();

	public void Init()
	{
	}

	public void SetUIParts(ChipListParts chipListParts, ChipSelectParts chipSelectParts)
	{
		this.chipListParts = chipListParts;
		this.chipSelectParts = chipSelectParts;

		UpdateChipParts();
	}

	public void UpdateChipParts()
	{
		chipListParts.UpdateChipParts(SelectChips());
	}

	public List<BaseChip> SelectChips()
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

	public void UpdateDeck()
	{

	}
}
