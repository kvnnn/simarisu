using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : GameMonoBehaviour
{
	[SerializeField]
	private GameManager gameManager;

	[SerializeField]
	private GameObject characterPrefab;

	private UserCharacter userCharacter;
	private List<MonsterCharacter> monsters = new List<MonsterCharacter>();

	public void Init()
	{
		DestroyAll();
	}

	public void ForDebug()
	{
		// For test
		userCharacter = AddCharacter<UserCharacter>(1);
		userCharacter.MoveTo(new Vector2(1,1), gameManager.stageManager.GetCellPosition(1,1));
		var mc = AddCharacter<MonsterCharacter>(2);
		mc.MoveTo(new Vector2(4,1), gameManager.stageManager.GetCellPosition(4,1));
		monsters.Add(mc);
	}

	public void UserCharacterAction(BaseChip chip, StageManager stageManager)
	{
		ActionCharacter(userCharacter, chip, stageManager);
	}

	public void ActionCharacter(BaseCharacter character, BaseChip chip, StageManager stageManager)
	{
		switch (chip.type)
		{
			case BaseChip.Type.Move:
				Vector2 movePosition = character.position + chip.position;
				if (IsMovable(movePosition, stageManager))
				{
					character.MoveTo(movePosition, stageManager.GetCellPosition(movePosition));
				}
			break;
			case BaseChip.Type.Attack:
			break;
			case BaseChip.Type.Cure:
			break;
			case BaseChip.Type.Other:
			break;
		}
	}

	public bool IsMovable(Vector2 movePosition, StageManager stageManager)
	{
		if (!stageManager.HasCell(movePosition)) {return false;}
		if (movePosition == userCharacter.position) {return false;}
		foreach (MonsterCharacter monster in monsters)
		{
			if (movePosition == monster.position) {return false;}
		}

		return true;
	}

	private T AddCharacter<T>(int characterId)
		where T : BaseCharacter
	{
		GameObject characterGameObject = Instantiate(characterPrefab);
		characterGameObject.transform.SetParent(transform);

		T character = characterGameObject.AddComponent<T>();
		character.Init(GetSprite(characterId));

		return character;
	}

	private Sprite GetSprite(int characterId)
	{
		string path = string.Format("Image/Character/{0}", characterId);
		return Resources.Load<Sprite>(path);
	}

	private void DestroyAll()
	{
		DestroyUser();
		DestroyAllMonster();
	}

	private void DestroyUser()
	{
		if (userCharacter == null) {return;}
		userCharacter.DestroyIfExist();
	}

	private void DestroyAllMonster()
	{
		if (monsters == null) {return;}
		foreach (MonsterCharacter monster in monsters)
		{
			monster.DestroyIfExist();
		}
		monsters = new List<MonsterCharacter>();
	}
}
