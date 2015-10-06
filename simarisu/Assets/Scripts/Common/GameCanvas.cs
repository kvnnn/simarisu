using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCanvas : GameMonoBehaviour
{
	void Awake()
	{
		Canvas canvas = gameObject.GetComponent<Canvas>();
		if (canvas.worldCamera == null)
		{
			canvas.worldCamera = Camera.main;
		}
	}
}
