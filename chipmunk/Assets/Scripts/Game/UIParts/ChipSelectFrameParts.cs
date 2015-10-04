using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChipSelectFrameParts : BaseUIParts
{
	private int chipIndex;

	[SerializeField]
	private Text text;

	private Image image
	{
		get {return gameObject.GetComponent<Image>();}
	}

	public void SetChip(int chipIndex, BaseChip chip)
	{
		this.chipIndex = chipIndex;
		UpdateParts(chip);
	}

	public void UpdateParts(BaseChip chip)
	{
		text.text = chip.chipName;
	}

	public void Focus()
	{
		image.color = Color.green;
	}

	public void Unfocus()
	{
		image.color = Color.white;
	}
}
