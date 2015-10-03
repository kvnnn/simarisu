using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChipListParts : BaseUIParts
{
	[SerializeField]
	private List<ChipParts> chipPartsLists;

	public void UpdateChipParts(List<BaseChip> chips)
	{
		int index = 0;
		foreach (BaseChip chip in chips)
		{
			ChipParts chipParts = chipPartsLists[index];
			chipParts.SetChip(chip);

			index++;
		}
	}
}
