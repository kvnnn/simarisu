using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacter : GameMonoBehaviour
{
	private HpLabelParts hpLabel;

	protected int maxHp;
	protected int hp;
	public int damage {get; private set;}

	public bool isDead
	{
		get {return hp <= 0;}
	}

	private RectTransform rectTransform
	{
		get {return (RectTransform)transform;}
	}
	private Image image
	{
		get {return gameObject.GetComponent<Image>();}
	}

	protected void Init(int maxHp, int damage)
	{
		this.maxHp = maxHp;
		this.hp = maxHp;
		this.damage = damage;
	}

	public void MoveTo(Vector2 position)
	{
		rectTransform.anchoredPosition = position;
		UpdateHpLabel();
	}

	public void SetSprite(Sprite sprite)
	{
		image.sprite = sprite;
	}

#region Damage/Cure
	public void Damage(int damage)
	{
		hp -= damage;
		hp = Mathf.Max(0, hp);
		hp = Mathf.Min(maxHp, hp);
		UpdateHpLabel();
	}

	public void Cure(int cure)
	{
		Damage(cure * -1);
	}
#endregion

#region HpText
	public void SetHpLabel(HpLabelParts label)
	{
		hpLabel = label;
		hpLabel.SetHp(maxHp);
	}

	private void UpdateHpLabel()
	{
		hpLabel.SetHp(hp);
		hpLabel.MoveTo(rectTransform.anchoredPosition);
	}
#endregion

	public void DestroyIfExist()
	{
		if (gameObject != null)
		{
			Destroy(hpLabel.gameObject);
			Destroy(gameObject);
		}
	}
}
