using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacter : GameMonoBehaviour
{
	protected int maxHp;
	protected int hp;
	protected int damage;

	private Card card;
	private int cardDamage
	{
		get {return card.damage + damage;}
	}
	private readonly Type[] RANGE_DETECTORS = new Type[]{typeof(CircleRangeDetector)};

	public bool isDead
	{
		get {return hp <= 0;}
	}

	private SpriteRenderer spriteRenderer
	{
		get {return gameObject.GetComponent<SpriteRenderer>();}
	}

	protected void Init(int maxHp, int damage, int order)
	{
		this.maxHp = maxHp;
		this.hp = maxHp;
		this.damage = damage;

		spriteRenderer.sortingOrder = order;
	}

	public void MoveTo(Vector2 position)
	{
		transform.position = position;
	}

	public void SetSprite(Sprite sprite)
	{
		spriteRenderer.sprite = sprite;
	}

#region Damage/Cure
	public void Damage(int damage)
	{
		hp -= damage;
		hp = Mathf.Max(0, hp);
		hp = Mathf.Min(maxHp, hp);

		if (isDead) {OnDead();}
	}

	protected virtual void OnDead() {}

	public void Cure(int cure)
	{
		Damage(cure * -1);
	}
#endregion

#region Card and AttackRange
	public void SetCard(Card card)
	{
		this.card = card;
		UpdateRange();
	}

	public void RemoveCard()
	{
		card = null;
		HideAllRange();
	}

	private void UpdateRange()
	{
		HideAllRange();

		BaseRangeDetector rangeDetector = gameObject.GetComponentsInChildren(RANGE_DETECTORS[card.rangeType], true)[0] as BaseRangeDetector;
		rangeDetector.Show((float)card.rangeSize, RangeDetectorOnTriggerEnter);
	}

	private void HideAllRange()
	{
		foreach (BaseRangeDetector rangeDetector in gameObject.GetComponentsInChildren<BaseRangeDetector>(true))
		{
			rangeDetector.Hide();
		}
	}

	private void RangeDetectorOnTriggerEnter(BaseCharacter character)
	{
		if (character == this) {return;}
		character.Damage(cardDamage);
	}
#endregion

	public void DestroyIfExist()
	{
		if (gameObject != null)
		{
			Destroy(gameObject);
		}
	}
}
