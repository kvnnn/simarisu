using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacter : GameMonoBehaviour
{
	protected int maxHp;
	protected int hp;
	public int damage {get; private set;}

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
	}

	public void Cure(int cure)
	{
		Damage(cure * -1);
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
