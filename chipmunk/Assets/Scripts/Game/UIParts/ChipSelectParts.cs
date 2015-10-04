using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChipSelectParts : BaseUIParts
{
	[SerializeField]
	private List<ChipSelectFrameParts> chipSelectFrames;

	private ChipSelectFrameParts focusParts;
	private int indexOfFocusParts
	{
		get {return chipSelectFrames.IndexOf(focusParts);}
	}
	private bool isLast
	{
		get {return chipSelectFrames.Count - 1 == indexOfFocusParts;}
	}

	private void UpdateFocusParts(ChipSelectFrameParts parts)
	{
		if (focusParts != null)
		{
			focusParts.Unfocus();
		}

		focusParts = parts;
		focusParts.Focus();
	}

	public void ResetFocus()
	{
		UpdateFocusParts(chipSelectFrames[0]);
	}

	public void SetChipToFocusSelectParts(int chipIndex, BaseChip chip)
	{
		focusParts.SetChip(chipIndex, chip);
		ChangeFocusToNextParts();
	}

	private void ChangeFocusToNextParts()
	{
		if (isLast) {return;}
		UpdateFocusParts(chipSelectFrames[indexOfFocusParts + 1]);
	}

	public void ChipSelectClick(ChipSelectFrameParts parts)
	{
		UpdateFocusParts(parts);
	}
}
