using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameViewManager : ViewManager
{
	[SerializeField]
	private GameManager gameManager;

	protected override void BeforeShow()
	{
		GameStartCoroutine(gameManager.InitGame());
	}

	protected override void AfterShow()
	{
		gameManager.StartGame();
	}
}
