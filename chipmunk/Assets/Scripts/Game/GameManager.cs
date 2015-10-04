using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : GameMonoBehaviour
{
	[SerializeField]
	private GameObject stagePrefab;
	public StageManager stageManager {get; private set;}

	[SerializeField]
	private CharacterManager characterManager;

	[SerializeField]
	private ChipManager chipManager;

	public void InitGame()
	{
		InitChip();
		InitStage();
		InitCharacter();
	}

	private void InitStage()
	{
		if (stageManager == null)
		{
			GameObject stageGameObject = Instantiate(stagePrefab);
			stageGameObject.transform.SetParent(transform.parent);
			stageManager = stageGameObject.GetComponent<StageManager>();
		}

		stageManager.Init();
	}

	private void InitCharacter()
	{
		characterManager.Init();
	}

	private void InitChip()
	{
		chipManager.Init();
	}

	public void StartGame()
	{
		characterManager.ForDebug();
	}

	public void InitUI(ChipListParts chipListParts, ChipSelectParts chipSelectParts)
	{
		chipManager.SetUIParts(chipListParts, chipSelectParts);
	}
}
