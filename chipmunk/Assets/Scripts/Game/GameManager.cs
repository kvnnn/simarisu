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

	private void StartBattle()
	{
		BeforeBattleStart();
		StartCoroutine(BattleCoroutine(AfterBattleStart));
	}

	private void BeforeBattleStart()
	{
		chipManager.ResetChipSelectFocus();
	}

	private void AfterBattleStart()
	{
		chipManager.UpdateParts();
	}

	private IEnumerator BattleCoroutine(System.Action callback)
	{
		int turn = 0;
		List<BaseChip> selectedChips = chipManager.GetSelectedChips();
		foreach (BaseChip chip in selectedChips)
		{
			yield return StartCoroutine(ExecuteTurnCoroutine(chip, turn));
			turn++;
			yield return new WaitForSeconds(1);
		}

		callback();
	}

	private IEnumerator ExecuteTurnCoroutine(BaseChip chip, int turn)
	{
		chipManager.FocusSelectParts(turn);
		characterManager.UserCharacterAction(chip, stageManager);
		yield return new WaitForSeconds(1);
	}

	public void InitUI(ChipListParts chipListParts, ChipSelectParts chipSelectParts, ButtonParts startBattleButtonParts)
	{
		startBattleButtonParts.buttonClick += StartBattleButtonClick;
		chipManager.SetUIParts(chipListParts, chipSelectParts, startBattleButtonParts);
	}

	public void StartBattleButtonClick(ButtonParts button)
	{
		StartBattle();
	}
}
