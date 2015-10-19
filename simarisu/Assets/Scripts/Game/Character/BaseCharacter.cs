using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacter : GameMonoBehaviour
{
	protected int maxHp;
	protected int hp;
	private int damage;
	private int damageUp = 0;
	public int cure {get; private set;}
	private int move;
	private int moveUp = 0;

	private StageCell cell;
	private HpLabelParts hpLabel;

	public bool isDead
	{
		get {return hp <= 0;}
	}

	private SpriteRenderer spriteRenderer
	{
		get {return gameObject.GetComponent<SpriteRenderer>();}
	}

	protected void Init(int maxHp, int damage, int cure, int move, int order)
	{
		this.maxHp = maxHp;
		this.hp = maxHp;
		this.damage = damage;
		this.cure = cure;
		this.move = move;

		ResetStatusUp();

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

	public Vector2 Position()
	{
		return cell.Position();
	}
#endregion

#region Damage Cure Move
	public int GetDamage()
	{
		return damage + damageUp;
	}

	public void AddDamageUp(int damageUp)
	{
		this.damageUp += damageUp;
	}

	public void Damage(int damage)
	{
		hp -= damage;
		hp = Mathf.Max(0, hp);
		hp = Mathf.Min(maxHp, hp);

		hpLabel.SetHp(hp);

		if (isDead) {OnDead();}
	}

	public void Cure(int cure)
	{
		Damage(cure * -1);
	}

	public int GetMove()
	{
		return move + moveUp;
	}

	public void AddMoveUp(int moveUp)
	{
		this.moveUp += moveUp;
	}

	private void ResetStatusUp()
	{
		damageUp = 0;
		moveUp = 0;
	}

	protected virtual void OnDead() {}
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
