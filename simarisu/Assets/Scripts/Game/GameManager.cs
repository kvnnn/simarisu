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
	private CardManager cardManager;

	private int point;
	private int turn;
	private int stageCount;
	private int stageIndex;
	private Stage currentStage;

	private const int MAX_MONSTER = 3;

	private GameStatus gameStatus = GameStatus.Standby;
	private enum GameStatus
	{
		Standby,
		Battle,
		Lose,
		Win,
	}

	public void InitGame()
	{
		ResetGameStatus();

		stageManager.Init();
		characterManager.Init();
		cardManager.Init();

		PrepareGame();
	}

#region Game
	public void PrepareGame()
	{
		currentStage = CheckAndGetCurrentStage();
		stageManager.LoadStage(currentStage.stageId);
		characterManager.PrepareGame();
	}

	public void StartGame()
	{
		characterManager.AddUserCharacter(stageManager.GetDefaultUserCharacterCell());
		characterManager.AddMonster(PickMonster(), stageManager.GetAvailableCells(characterManager.GetCharacterCells()));
	}

	private void StartBattle()
	{
		BeforeBattleStart();
		StartCoroutine(BattleCoroutine(AfterBattle));
	}

	private void BeforeBattleStart()
	{
		gameStatus = GameStatus.Battle;
		cardManager.EnableTouchEvent(false);
	}

	private void AfterBattle()
	{
		gameStatus = GameStatus.Standby;
		cardManager.EnableTouchEvent(true);
		cardManager.UpdateParts();

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

		turn++;
	}

	private IEnumerator BattleCoroutine(System.Action callback)
	{
		yield return StartCoroutine(UserCharacterBattleCoroutine());
		if (IsGameFinish())
		{
			callback();
			yield break;
		}
		yield return new WaitForSeconds(0.5f);

		yield return StartCoroutine(MonsterBattleCoroutine());

		callback();
	}

	private IEnumerator UserCharacterBattleCoroutine()
	{
		List<Card> selectedCards = cardManager.GetSelectedCards();
		bool isMoveDone = false;

		yield return StartCoroutine(characterManager.UserCharacterAction(selectedCards[0]));

		// characterManager.MoveUserCharacter(
		// 	selectedCards[1],
		// 	lineManager.movePointList.ToArray(),
		// 	()=>{isMoveDone = true;}
		// );

		while (!isMoveDone)
		{
			yield return null;
		}

		yield return StartCoroutine(characterManager.UserCharacterAction(selectedCards[2]));

		yield return null;
	}

	private IEnumerator MonsterBattleCoroutine()
	{
		yield return null;
	}

	private void Win()
	{
		point++;
		stageCount++;
		PrepareGame();
		StartGame();
	}

	private void Lose()
	{
		ResetGameStatus();
		PrepareGame();
		StartGame();
	}
#endregion

#region GameStatus
	private void ResetGameStatus()
	{
		gameStatus = GameStatus.Standby;

		point = 0;
		turn = 0;
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

		return gameStatus == GameStatus.Win || gameStatus == GameStatus.Lose;
	}

	private bool isStandby
	{
		get {return gameStatus == GameStatus.Standby;}
	}

	private bool isBattle
	{
		get {return gameStatus == GameStatus.Battle;}
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
	public void InitUI(CardListParts cardListParts, ButtonParts startBattleButtonParts)
	{
		startBattleButtonParts.buttonClick += StartBattleButtonClick;
		cardManager.SetUIParts(cardListParts, startBattleButtonParts);
	}
#endregion

#region Convert Position
	public Vector3 GetWorldPoint(Vector3 position)
	{
		position = Camera.main.ScreenToWorldPoint(position);
		position.z = 0;
		return position;
	}
#endregion

#region Event
	private void StartBattleButtonClick(ButtonParts button)
	{
		StartBattle();
	}
#endregion
}
