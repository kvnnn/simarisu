using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : GameMonoBehaviour
{
	[SerializeField]
	private Background background;
	[SerializeField]
	private CharacterManager characterManager;
	[SerializeField]
	private CardManager cardManager;
	[SerializeField]
	private LineManager lineManager;

	private bool isDrawing = false;
	private bool forceEndDrawing = false;

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

	public void InitGame()
	{
		ResetGameStatus();

		background.Init(BackgroundOnExit);
		characterManager.Init();
		cardManager.Init();
		lineManager.Init();

		PrepareGame();
	}

#region Game
	public void PrepareGame()
	{
		currentStage = CheckAndGetCurrentStage();
		characterManager.DestroyAll();
	}

	public void StartGame()
	{
		characterManager.AddMonster(PickMonster());
		characterManager.AddUserCharacter(CharacterOnBeginDrag, CharacterOnDrag, CharacterOnEndDrag);
	}

	private void StartBattle()
	{
		BeforeBattleStart();
		StartCoroutine(BattleCoroutine(AfterBattleStart));
	}

	private void BeforeBattleStart()
	{
		// cardManager.ResetCardSelectFocus();
	}

	private void AfterBattleStart()
	{
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
	}

	private IEnumerator BattleCoroutine(System.Action callback)
	{
		gameStatus = GameStatus.Battle;
		int turn = 0;
		List<Card> selectedCards = cardManager.GetSelectedCards();
		foreach (Card card in selectedCards)
		{
			turn++;
			totalTurnCount++;
			yield return StartCoroutine(ExecuteTurnCoroutine(card, turn));
			if (IsGameFinish()) {break;}
		}

		callback();
	}

	private IEnumerator ExecuteTurnCoroutine(Card card, int turn)
	{
		characterManager.UserCharacterAction(card);

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
	public void StartBattleButtonClick(ButtonParts button)
	{
		StartBattle();
	}

	private void BackgroundOnExit()
	{
		forceEndDrawing = true;
	}

	private void CharacterOnBeginDrag(Vector3 position)
	{
		forceEndDrawing = false;
		isDrawing = true;
		lineManager.StartDrawing(GetWorldPoint(position));
	}

	private void CharacterOnDrag(Vector3 position)
	{
		if (!isDrawing) {return;}

		if (!forceEndDrawing)
		{
			lineManager.AddPoint(GetWorldPoint(position));
		}
		else
		{
			CharacterOnEndDrag(position);
		}
	}

	private void CharacterOnEndDrag(Vector3 position)
	{
		if (!isDrawing) {return;}

		isDrawing = false;
		lineManager.EndDrawing(GetWorldPoint(position));
	}
#endregion
}
