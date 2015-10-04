using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChipSelectFrameParts : BaseUIParts
{
	private int chipIndex = DEFAULT_INDEX;
	private const int DEFAULT_INDEX = -1;
	public bool isSet
	{
		get {return chipIndex != DEFAULT_INDEX;}
	}

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

	public void RemoveChip()
	{
		chipIndex = DEFAULT_INDEX;
		UpdateParts(null);
	}

	public int GetChipIndex()
	{
		return chipIndex;
	}

	public void UpdateParts(BaseChip chip)
	{
		if (chip != null)
		{
			text.text = chip.chipName;
		}
		else
		{
			text.text = "";
		}
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
