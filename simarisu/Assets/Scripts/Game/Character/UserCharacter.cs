using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UserCharacter : BaseCharacter
{
	private User user;
	public float maxDrawing {get; private set;}
	public const float DEFAULT_MAX_DRAWING = 10f;

	public void Init(User user, int order)
	{
		this.user = user;
		this.maxDrawing = DEFAULT_MAX_DRAWING;

		base.Init(user.hp, user.damage, order);

		// base.Init(user.hp, 100); // For Debug
	}
}
