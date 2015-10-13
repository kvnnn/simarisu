using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HpLabelParts : BaseUIParts
{
	[SerializeField]
	private Text text;
	private readonly Vector2 OFFSET = new Vector2(0, -34.5f);

	public void SetHp(int hp)
	{
		hp = Mathf.Max(0, hp);
		text.text = hp.ToString();
	}

	public void MoveTo(Vector2 position)
	{
		position += OFFSET;
		RectTransform rectTransform = transform as RectTransform;
		rectTransform.anchoredPosition = position;
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
