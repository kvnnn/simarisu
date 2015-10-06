using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : GameMonoBehaviour
{
	[SerializeField]
	private StageManager stageManager;
	[SerializeField]
	private CharacterManager characterManager;
	[SerializeField]
	private ChipManager chipManager;

	private int point;
	private int totalTurnCount;
	private int stageCount;
	private int stageIndex;
	private Stage currentStage;

	private const int MAX_MONSTER = 3;

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

		PrepareGame();
	}

	private void InitStage()
	{
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
	public void PrepareGame()
	{
		currentStage = CheckAndGetCurrentStage();
		characterManager.DestroyAll();
	}

	public void StartGame()
	{
		characterManager.AddUserCharacter();
		characterManager.AddMonster(PickMonster());
	}

	private void StartBattle()
	{
		BeforeBattleStart();
		StartCoroutine(BattleCoroutine(AfterBattleStart));
	}

	private void BeforeBattleStart()
	{
		// chipManager.ResetChipSelectFocus();
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
		characterManager.UserCharacterAction(chip);

		yield return new WaitForSeconds(1);
		if (IsGameFinish()) {yield break;}

		characterManager.MonsterActions();

		yield return new WaitForSeconds(1);
	}

	public void Win()
	{
		stageCount++;
		PrepareGame();
		StartGame();
	}

	public void Lose()
	{
		ResetGameStatus();
		PrepareGame();
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

#region Stage
	private Stage CheckAndGetCurrentStage()
	{
		CheckStage();
		return GetCurrentStage();
	}

	private Stage GetCurrentStage()
	{
		return Stage.GetAllStage()[stageIndex];
	}

	private void CheckStage()
	{
		Stage stageData = GetCurrentStage();
		bool isProperStage = stageData.maxRange >= stageCount && stageCount >= stageData.minRange;

		if (!isProperStage)
		{
			stageIndex++;
			CheckStage();
		}
	}

	private List<Monster> PickMonster()
	{
		List<Monster> monsterList = currentStage.monsters;
		int numSelect = Random.Range(1, Mathf.Min(monsterList.Count, MAX_MONSTER));

		System.Random random = new System.Random();
		return monsterList.OrderBy(x => random.Next()).Take(numSelect).ToList();
	}
#endregion

#region UIParts
	public void InitUI(ChipListParts chipListParts, ButtonParts startBattleButtonParts)
	{
		startBattleButtonParts.buttonClick += StartBattleButtonClick;
		chipManager.SetUIParts(chipListParts, startBattleButtonParts);
	}
#endregion

#region Event
	public void StartBattleButtonClick(ButtonParts button)
	{
		StartBattle();
	}
#endregion
}
