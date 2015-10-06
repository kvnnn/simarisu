using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ButtonParts : BaseUIParts
{
	[SerializeField]
	private Text text;

	private Image image
	{
		get {return gameObject.GetComponent<Image>();}
	}
	private Button button
	{
		get {return gameObject.GetComponent<Button>();}
	}

	public System.Action<ButtonParts> buttonClick;

	public void ButtonClick()
	{
		buttonClick(this);
	}

	public bool isEnabled
	{
		set
		{
			button.interactable = value;
		}
	}
}
