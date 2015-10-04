using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterManager : GameMonoBehaviour
{
	[SerializeField]
	private GameObject characterPrefab;
	[SerializeField]
	private GameObject hpLabelPrefab;

	[SerializeField]
	private Transform uiBaseTransform;
	[SerializeField]
	private Canvas uiBaseCanvas;
	private Camera uiCamera
	{
		get {return uiBaseCanvas.worldCamera;}
	}

	private UserCharacter userCharacter;
	private List<MonsterCharacter> monsters = new List<MonsterCharacter>();
	private List<BaseCharacter> allCharacters
	{
		get
		{
			List<BaseCharacter> list = monsters.ConvertAll(c => (BaseCharacter)c);;
			list.Add(userCharacter);
			return list;
		}
	}

	private const string DEFAULT_USER_POSITION = "1,1";
	private List<string> DEFAULT_MONSTER_POSITION = new List<string>(){"3,0","4,0","5,0","3,1","4,1","5,1","3,2","4,2","5,2"};

	public void Init()
	{
		DestroyAll();
	}

#region CharacterStatus
	public bool UserCharacterDead()
	{
		return userCharacter.isDead;
	}

	public bool MonsterAllDead()
	{
		foreach (MonsterCharacter monster in monsters)
		{
			if (!monster.isDead) {return false;}
		}
		return true;
	}
#endregion

#region CharacterAction
	public void UserCharacterAction(Chip chip, StageManager stageManager)
	{
		ActionCharacter(userCharacter, chip, stageManager);
	}

	public void MonsterActions(StageManager stageManager)
	{
		foreach (MonsterCharacter monster in monsters)
		{
			Chip chip = monster.SelectChip();
			ActionCharacter(monster, chip, stageManager);
		}
	}

	public void ActionCharacter(BaseCharacter character, Chip chip, StageManager stageManager)
	{
		if (chip == null) {return;}
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
				foreach (BaseCharacter target in GetCharacterInRange(character.position, chip.position, chip.range, character.directionInt))
				{
					if (target == character) {continue;}
					target.Damage(CalculateDamage(character, chip));
					if (target is MonsterCharacter && target.isDead)
					{
						DestroyMonster(target as MonsterCharacter);
					}
				}
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

		foreach (BaseCharacter character in allCharacters)
		{
			if (movePosition == character.position) {return false;}
		}

		return true;
	}

	public List<BaseCharacter> GetCharacterInRange(Vector2 currentPosition, Vector2 attackPosition, Vector2 range, int direction)
	{
		List<BaseCharacter> targetCharacters = new List<BaseCharacter>();
		Vector2 position = currentPosition + attackPosition.MultiplyX(direction);

		// Ignore range for this time

		foreach (BaseCharacter character in allCharacters)
		{
			if (position == character.position)
			{
				targetCharacters.Add(character);
			}
		}

		return targetCharacters;
	}

	public int CalculateDamage(BaseCharacter character, Chip chip)
	{
		int damage = chip.damage;
		if (damage == 0)
		{
			damage = character.damage;
		}
		return damage;
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
		character.getUIPosition = GetUIPosition;

		GameObject hpLabelGo = Instantiate(hpLabelPrefab);
		hpLabelGo.transform.SetParent(uiBaseTransform);
		character.SetHpLabel(hpLabelGo.GetComponent<HpLabelParts>());

		return character;
	}

	public void AddUserCharacter(StageManager stageManager)
	{
		Vector2 defaultPos = GameVector.GetFromString(DEFAULT_USER_POSITION);
		userCharacter = AddUserCharacter(User.GetUser());
		userCharacter.MoveTo(defaultPos, stageManager.GetCellPosition(defaultPos));
	}

	private UserCharacter AddUserCharacter(User data)
	{
		UserCharacter character = AddCharacter<UserCharacter>(data.sprite);
		character.Init(data);
		return character;
	}

	public void AddMonster(List<Monster> monsterList, StageManager stageManager)
	{
		int count = monsterList.Count;
		System.Random random = new System.Random();
		List<string> positionList = DEFAULT_MONSTER_POSITION.OrderBy(x => random.Next()).Take(count).ToList();

		int index = 0;
		foreach (Monster monster in monsterList)
		{
			Vector2 defaultPos = GameVector.GetFromString(positionList[index]);

			MonsterCharacter mc = AddMonster(monster);
			mc.MoveTo(defaultPos, stageManager.GetCellPosition(defaultPos));
			monsters.Add(mc);
			index++;
		}
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

	public void DestroyAll()
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

	private void DestroyMonster(MonsterCharacter monster)
	{
		monsters.Remove(monster);
		monster.DestroyIfExist();
	}
#endregion

#region Position of World/UI
	private Vector2 GetUIPosition(Vector3 position)
	{
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
		Vector2 uiPosition = Vector2.zero;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(uiBaseCanvas.transform as RectTransform, screenPosition, uiCamera, out uiPosition);
		return uiPosition;
	}
#endregion
}
