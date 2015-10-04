using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacter : GameMonoBehaviour
{
	public Vector2 position {get; private set;}

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

	protected void Init()
	{
		SetDirection(defaultDirection);
	}

	public void MoveTo(Vector2 position, Vector3 coordinate)
	{
		this.position = position;
		transform.MoveTo(coordinate);
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
