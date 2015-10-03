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

	private ChipListParts chipListParts;
	public bool isUIPartsInstantiated
	{
		get {return chipListParts != null;}
	}

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

	public void InitUI(ChipListParts chipListParts)
	{
		this.chipListParts = chipListParts;
	}
}
