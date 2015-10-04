using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserCharacter : BaseCharacter
{
	private User user;

	public void Init(User user)
	{
		this.user = user;
		base.Init(user.hp, user.damage);
	}
}
