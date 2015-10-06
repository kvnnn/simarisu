using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacter : GameMonoBehaviour
{
	private HpLabelParts hpLabel;
	public Vector2 position {get; private set;}

	protected int maxHp;
	protected int hp;
	public int damage {get; private set;}

	public bool isDead
	{
		get {return hp <= 0;}
	}

	public System.Func<Vector3, Vector2> getUIPosition;

	private SpriteRenderer spriteRenderer
	{
		get {return gameObject.GetComponent<SpriteRenderer>();}
	}

	private Direction direction;
	public int directionInt
	{
		get {return (int)direction;}
	}
	protected virtual Direction defaultDirection
	{
		get {return Direction.Right;}
	}
	protected enum Direction
	{
		Right = 1,
		Left = -1,
	}

	protected void Init(int maxHp, int damage)
	{
		SetDirection(defaultDirection);

		this.maxHp = maxHp;
		this.hp = maxHp;
		this.damage = damage;
	}

	public void MoveTo(Vector2 position, Vector3 coordinate)
	{
		this.position = position;
		transform.MoveTo(coordinate);
		UpdateHpLabel();
	}

	public void SetSprite(Sprite sprite)
	{
		spriteRenderer.sprite = sprite;
	}

	protected void SetDirection(Direction direction)
	{
		this.direction = direction;
		transform.RotateY(directionInt == 1 ? 0 : 180);
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
		hpLabel.MoveTo(getUIPosition(transform.position));
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
