using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CharacterManager : GameMonoBehaviour
{
	[SerializeField]
	private GameObject characterPrefab;

	private UserCharacter userCharacter;
	private List<MonsterCharacter> monsters = new List<MonsterCharacter>();
	private List<BaseCharacter> allCharacters
	{
		get
		{
			List<BaseCharacter> list = monsters.ConvertAll(c => (BaseCharacter)c);
			list.Add(userCharacter);
			return list;
		}
	}

	private int sortingOrder = 0;

	public void Init()
	{
		PrepareGame();
	}

	public void PrepareGame()
	{
		DestroyAll();
		sortingOrder = 0;
	}

#region CharacterStatus
	public bool UserCharacterDead()
	{
		return userCharacter.isDead;
	}

	public int UserCharacterMaxDrawing()
	{
		return userCharacter.maxDrawing;
	}

	public bool MonsterAllDead()
	{
		foreach (MonsterCharacter monster in monsters)
		{
			if (monster == null) {continue;}
			if (!monster.isDead) {return false;}
		}
		return true;
	}
#endregion

#region CharacterCell
	public List<StageCell> GetCharacterCells()
	{
		List<StageCell> cells = new List<StageCell>();
		foreach (BaseCharacter character in allCharacters)
		{
			cells.Add(character.GetCurrentCell());
		}

		return cells;
	}

	public bool IsCellAvilable(StageCell cell)
	{
		foreach (BaseCharacter character in allCharacters)
		{
			if (character.GetCurrentCell() == cell) {return false;}
		}
		return true;
	}

	public StageCell GetUserCharacterCell()
	{
		return userCharacter.GetCurrentCell();
	}
#endregion

#region CharacterAction
	public IEnumerator MoveUserCharacter(Card card, StageCell cell, System.Action callback)
	{
		yield return StartCoroutine(ActionCharacter(userCharacter, card, true));

		LeanTween.move(userCharacter.gameObject, cell.PositionInWorld(), 0.5f).setOnComplete(
			()=> {
				userCharacter.MoveTo(cell, false);
				callback();
			}
		);
	}

	public IEnumerator UserCharacterAction(Card card)
	{
		yield return StartCoroutine(ActionCharacter(userCharacter, card));
	}

	public IEnumerator MonsterActions()
	{
		foreach (MonsterCharacter monster in monsters)
		{
			if (monster == null) {continue;}
			Card card = monster.SelectCard();
			yield return StartCoroutine(ActionCharacter(monster, card));
		}
	}

	public IEnumerator ActionCharacter(BaseCharacter character, Card card, bool isStartMoving = false)
	{
		if (card == null) {yield break;}
		switch (card.type)
		{
			case Card.Type.Attack:
				character.SetCard(card);
			break;
			case Card.Type.Support:
			break;
			case Card.Type.Other:
			break;
		}

		if (isStartMoving) {yield return null;}
		else
		{
			yield return new WaitForSeconds(0.5f);
			character.RemoveCard();
		}
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

	public void AddUserCharacter(StageCell cell)
	{
		userCharacter = AddUserCharacter(User.GetUser());
		userCharacter.MoveTo(cell);
	}

	private UserCharacter AddUserCharacter(User data)
	{
		UserCharacter character = AddCharacter<UserCharacter>(data.sprite);
		character.Init(data, sortingOrder);
		sortingOrder++;
		return character;
	}

	public void AddMonster(List<Monster> monsterList, List<StageCell> cells)
	{
		int count = monsterList.Count;
		System.Random random = new System.Random();
		List<StageCell> cellList = cells.OrderBy(x => random.Next()).Take(count).ToList();

		int index = 0;
		foreach (Monster monster in monsterList)
		{
			MonsterCharacter mc = AddMonster(monster);
			mc.MoveTo(cellList[index]);
			monsters.Add(mc);
			index++;
		}
	}

	private MonsterCharacter AddMonster(Monster data)
	{
		MonsterCharacter monster = AddCharacter<MonsterCharacter>(data.sprite);
		monster.Init(data, sortingOrder);
		monster.onDead = DestroyMonster;
		sortingOrder++;
		return monster;
	}

	private Sprite GetSprite(string spriteId)
	{
		string path = string.Format("Characters/Images/{0}", spriteId);
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
			if (monster == null) {continue;}
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
}
