using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacter : GameMonoBehaviour
{
	protected int maxHp;
	protected int hp;
	protected int damage;

	private StageCell cell;
	private HpLabelParts hpLabel;

	private Card card;
	private int cardDamage
	{
		get {return card.damage + damage;}
	}

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

		UpdateHpLabel();
		spriteRenderer.sortingOrder = order;
	}

	public void SetSprite(Sprite sprite)
	{
		spriteRenderer.sprite = sprite;
	}

#region Cell
	public void MoveTo(StageCell stageCell, System.Func<Vector3, Vector2> convertToCanvasPosition, bool movePosition = true)
	{
		this.cell = stageCell;

		hpLabel.MoveTo(convertToCanvasPosition(cell.PositionInWorld()));

		if (movePosition)
		{
			transform.position = cell.PositionInWorld();
		}
	}

	public StageCell GetCurrentCell()
	{
		return cell;
	}
#endregion

#region Damage/Cure
	public void Damage(int damage)
	{
		hp -= damage;
		hp = Mathf.Max(0, hp);
		hp = Mathf.Min(maxHp, hp);

		hpLabel.SetHp(hp);

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
	}

	public void RemoveCard()
	{
		card = null;
	}
#endregion

#region HpLabel
	public void SetHpLabel(HpLabelParts hpLabel)
	{
		this.hpLabel = hpLabel;
	}

	private void UpdateHpLabel()
	{
		hpLabel.SetHp(hp);
	}

	public void ShowHpLabel()
	{
		hpLabel.Show();
	}

	public void HideHpLabel()
	{
		hpLabel.Hide();
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
