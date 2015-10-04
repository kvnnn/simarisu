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
	protected int damage;

	public System.Func<Vector3, Vector2> getUIPosition;

	private SpriteRenderer spriteRenderer
	{
		get {return gameObject.GetComponent<SpriteRenderer>();}
	}

	protected virtual Direction defaultDirection
	{
		get {return Direction.Right;}
	}
	protected enum Direction
	{
		Right = 0,
		Left = 1,
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

	protected void SetDirection(Direction direction)
	{
		transform.RotateY(180 * (int)direction);
	}

	public void DestroyIfExist()
	{
		if (gameObject != null)
		{
			Destroy(hpLabel.gameObject);
			Destroy(gameObject);
		}
	}
}
