using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChipParts : BaseUIParts
{
	private BaseChip chip;

	public void SetChip(BaseChip chip)
	{
		this.chip = chip;
		UpdateParts();
	}

	public void UpdateParts()
	{

	}
}
