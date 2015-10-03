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

	public ChipListParts chipListParts {get; private set;}
	public ChipSelectParts chipSelectParts {get; private set;}

	public IEnumerator InitGame()
	{
		InitStage();
		yield return new WaitForEndOfFrame();
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

	public void StartGame()
	{

	}

	public void InitUI(ChipListParts chipListParts, ChipSelectParts chipSelectParts)
	{
		this.chipListParts = chipListParts;
		this.chipSelectParts = chipSelectParts;
	}
}
