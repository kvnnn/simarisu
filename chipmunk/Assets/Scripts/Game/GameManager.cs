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

	private int point;
	private int totalTurnCount;
	private int stageCount;
	private int stageIndex;

	private GameStatus gameStatus;
	private enum GameStatus
	{
		Battle,
		Lose,
		Win,
	}

#region InitManager
	public void InitGame()
	{
		ResetGameStatus();

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
#endregion

#region Game
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

		if (IsGameFinish())
		{
			if (gameStatus == GameStatus.Win)
			{
				Win();
			}
			else
			{
				Lose();
			}
		}
	}

	private IEnumerator BattleCoroutine(System.Action callback)
	{
		gameStatus = GameStatus.Battle;
		int turn = 0;
		List<Chip> selectedChips = chipManager.GetSelectedChips();
		foreach (Chip chip in selectedChips)
		{
			turn++;
			totalTurnCount++;
			yield return StartCoroutine(ExecuteTurnCoroutine(chip, turn));
			if (IsGameFinish()) {break;}
		}

		callback();
	}

	private IEnumerator ExecuteTurnCoroutine(Chip chip, int turn)
	{
		chipManager.FocusSelectParts(turn - 1);
		characterManager.UserCharacterAction(chip, stageManager);

		yield return new WaitForSeconds(1);
		if (IsGameFinish()) {yield break;}

		characterManager.MonsterActions(stageManager);

		yield return new WaitForSeconds(1);
	}

	public void Win()
	{
		characterManager.Init();
		StartGame();
	}

	public void Lose()
	{
		characterManager.Init();
		StartGame();
	}
#endregion

#region GameStatus
	private void ResetGameStatus()
	{
		point = 0;
		totalTurnCount = 0;
		stageCount = 1;
		stageIndex = 0;
	}

	private bool IsGameFinish()
	{
		if (characterManager.UserCharacterDead())
		{
			gameStatus = GameStatus.Lose;
		}
		else if (characterManager.MonsterAllDead())
		{
			gameStatus = GameStatus.Win;
		}
		else
		{
			gameStatus = GameStatus.Battle;
		}

		return gameStatus != GameStatus.Battle;
	}
#endregion

#region UIParts
	public void InitUI(ChipListParts chipListParts, ChipSelectParts chipSelectParts, ButtonParts startBattleButtonParts)
	{
		startBattleButtonParts.buttonClick += StartBattleButtonClick;
		chipManager.SetUIParts(chipListParts, chipSelectParts, startBattleButtonParts);
	}
#endregion

#region Event
	public void StartBattleButtonClick(ButtonParts button)
	{
		StartBattle();
	}
#endregion
}
