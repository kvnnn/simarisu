using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HpLabelParts : BaseUIParts
{
	[SerializeField]
	private Text text;
	private Vector2 offset = new Vector2(0, -11f);

	public void SetHp(int hp)
	{
		hp = Mathf.Max(0, hp);
		text.text = hp.ToString();
	}

	public void MoveTo(Vector2 position)
	{
		position += offset;
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.anchoredPosition = position;
	}
}
