using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : GameMonoBehaviour
{
	[SerializeField]
	private GameObject stagePrefab;
	private StageManager stageManager;

	public void InitGame()
	{
		InitStage();
	}

	private void InitStage()
	{
		if (stageManager == null) {
			GameObject stageGameObject = Instantiate(stagePrefab);
			stageGameObject.transform.SetParent(transform.parent);
			stageManager = stageGameObject.GetComponent<StageManager>();
		}

		stageManager.Init();
	}

	public void StartGame()
	{

	}
}
