using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UserCharacter : BaseCharacter, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private User user;

	public System.Action<Vector3> onBeginDrag;
	public System.Action<Vector3> onDrag;
	public System.Action<Vector3> onEndDrag;

	public void Init(User user)
	{
		this.user = user;
		base.Init(user.hp, user.damage);

		// base.Init(user.hp, 100); // For Debug
	}

#region Event
	public void OnBeginDrag(PointerEventData eventData)
	{
		onBeginDrag(eventData.position);
	}

	public void OnDrag(PointerEventData eventData)
	{
		onDrag(eventData.position);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		onEndDrag(eventData.position);
	}
#endregion
}
