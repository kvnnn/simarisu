using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UserCharacter : BaseCharacter
{
	private User user;

	public void Init(User data, int order)
	{
		this.user = data;

		base.Init(user.hp, user.damage, user.cure, user.move, order);

		// base.Init(user.hp, 100); // For Debug
	}
}
