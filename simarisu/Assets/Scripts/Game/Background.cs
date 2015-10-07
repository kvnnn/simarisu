using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Background : GameMonoBehaviour, IPointerExitHandler
{
	[SerializeField]
	private Image image;

	private System.Action onExit;

	public void Init(System.Action onExit)
	{
		this.onExit = onExit;
	}

#region Event
	public void OnPointerExit(PointerEventData eventData)
	{
		onExit();
	}
#endregion
}
