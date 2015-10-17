using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UserCharacter : BaseCharacter
{
	private User user;
	public int maxDrawing {get; private set;}
	public const int DEFAULT_MAX_DRAWING = 5;

	public void Init(User data, int order)
	{
		this.user = data;
		this.maxDrawing = DEFAULT_MAX_DRAWING;

		base.Init(user.hp, user.damage, user.cure, order);

		// base.Init(user.hp, 100); // For Debug
	}
}
