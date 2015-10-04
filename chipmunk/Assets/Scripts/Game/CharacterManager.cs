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
		userCharacter = AddUserCharacter(1);
		userCharacter.MoveTo(new Vector2(1,1), gameManager.stageManager.GetCellPosition(1,1));

		var mc = AddMonster(Monster.GetMonster(0));
		mc.MoveTo(new Vector2(4,1), gameManager.stageManager.GetCellPosition(4,1));
		monsters.Add(mc);
	}

#region CharacterAction
	public void UserCharacterAction(Chip chip, StageManager stageManager)
	{
		ActionCharacter(userCharacter, chip, stageManager);
	}

	public void ActionCharacter(BaseCharacter character, Chip chip, StageManager stageManager)
	{
		switch (chip.type)
		{
			case Chip.Type.Move:
				Vector2 movePosition = character.position + chip.position;
				if (IsMovable(movePosition, stageManager))
				{
					character.MoveTo(movePosition, stageManager.GetCellPosition(movePosition));
				}
			break;
			case Chip.Type.Attack:
			break;
			case Chip.Type.Cure:
			break;
			case Chip.Type.Other:
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
#endregion

#region Add/Delete Character
	private T AddCharacter<T>(string spriteId)
		where T : BaseCharacter
	{
		GameObject characterGameObject = Instantiate(characterPrefab);
		characterGameObject.transform.SetParent(transform);

		T character = characterGameObject.AddComponent<T>();
		character.SetSprite(GetSprite(spriteId));

		return character;
	}

	private UserCharacter AddUserCharacter(int characterId)
	{
		// For debug
		UserCharacter character = AddCharacter<UserCharacter>(characterId.ToString());
		character.Init(null);
		return character;
	}

	private MonsterCharacter AddMonster(Monster data)
	{
		MonsterCharacter monster = AddCharacter<MonsterCharacter>(data.sprite);
		monster.Init(data);
		return monster;
	}

	private Sprite GetSprite(string spriteId)
	{
		string path = string.Format("Image/Character/{0}", spriteId);
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
#endregion
}
