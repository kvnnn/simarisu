using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChipListParts : BaseUIParts
{
	[SerializeField]
	private List<ChipParts> chipPartsLists;

	public System.Action<int, Chip> chipPartsClick;

	public void SetChips(List<Chip> chips)
	{
		RemoveAllChip();

		int index = 0;
		foreach (Chip chip in chips)
		{
			ChipParts chipParts = chipPartsLists[index];
			chipParts.SetChip(chip);

			index++;
		}

		UpdateAllChip();
	}

	public Chip GetChip(int index)
	{
		return chipPartsLists[index].GetChip();
	}

	public void UpdateAllChip()
	{
		foreach (ChipParts parts in chipPartsLists)
		{
			parts.UpdateParts();
		}
	}

	public void RemoveAllChip()
	{
		foreach (ChipParts parts in chipPartsLists)
		{
			parts.RemoveChip();
		}
	}

#region Event
	public void ChipClick(ChipParts parts)
	{
		chipPartsClick(chipPartsLists.IndexOf(parts), parts.GetChip());
	}
#endregion
}
