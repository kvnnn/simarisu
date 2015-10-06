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
	}

#region Event
	public void OnPointerExit(PointerEventData eventData)
	{
		// Debug.Log(eventData.position);
	}
#endregion
}
