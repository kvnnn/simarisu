using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StageManager : GameMonoBehaviour, IPointerExitHandler
{
	[SerializeField]
	private Image stageImage;

	public void Init()
	{
		Canvas canvas = gameObject.GetComponent<Canvas>();
		canvas.sortingOrder = -6;
	}

#region Event
	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("マウスアウト position=" + eventData.position);
	}
#endregion
}
