using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChipParts : BaseUIParts
{
	private BaseChip chip;

	[SerializeField]
	private Text text;

	public void SetChip(BaseChip chip)
	{
		this.chip = chip;
		UpdateParts();
	}

	public void UpdateParts()
	{
		text.text = chip.chipName;
	}
}
