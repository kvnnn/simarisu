using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Background : GameMonoBehaviour, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField]
	private Image image;

	public System.Action onExit;
	public System.Action<Vector3> onBeginDrag;
	public System.Action<Vector3> onDrag;
	public System.Action<Vector3> onEndDrag;

#region Event
	public void OnPointerExit(PointerEventData eventData)
	{
		onExit();
	}

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
