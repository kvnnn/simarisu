using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChipParts : BaseUIParts
{
	private Chip chip;
	public bool hasChip
	{
		get {return chip != null;}
	}

	[SerializeField]
	private Text text;

	public void SetChip(Chip chip)
	{
		this.chip = chip;
	}

	public Chip GetChip()
	{
		return chip;
	}

	public void RemoveChip()
	{
		chip = null;
	}

	public void UpdateParts()
	{
		if (hasChip) {
			gameObject.SetActive(true);
			text.text = chip.chipName;
		} else {
			gameObject.SetActive(false);
			text.text = "";
		}
	}
}
