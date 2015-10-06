using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UserCharacter : BaseCharacter, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private User user;

	public void Init(User user)
	{
		this.user = user;
		base.Init(user.hp, user.damage);

		// base.Init(user.hp, 100); // For Debug
	}

#region Event
	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log("マウスドラッグ開始 position=" + eventData.position);
	}

	public void OnDrag(PointerEventData eventData) {
		Debug.Log("マウスドラッグ中 position=" + eventData.position);
	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log("マウスドラッグ終了 position=" + eventData.position);
	}
#endregion
}
