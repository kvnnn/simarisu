using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacter : GameMonoBehaviour
{
	private SpriteRenderer spriteRenderer;

	protected virtual Direction defaultDirection
	{
		get {return Direction.Right;}
	}
	protected enum Direction
	{
		Right = 0,
		Left = 1,
	}

	public virtual void Init(Sprite sprite)
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		SetDirection(defaultDirection);
		SetSprite(sprite);
	}

	public void SetSprite(Sprite sprite)
	{
		spriteRenderer.sprite = sprite;
	}

	protected void SetDirection(Direction direction)
	{
		transform.RotateY(180 * (int)direction);
	}

	public void DestroyIfExist()
	{
		if (gameObject != null)
		{
			Destroy(gameObject);
		}
	}
}
