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

	private readonly Vector2 DEFAULT_USER_POSITION = new Vector2(0,0);
	private readonly List<Vector2> DEFAULT_MONSTER_POSITION = new List<Vector2>(){new Vector2(-2, 3), new Vector2(-2, 0), new Vector2(-2, -2), new Vector2(0, 3), new Vector2(0, -2), new Vector2(2, 3), new Vector2(2, 0), new Vector2(2, -2)};

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

	public float UserCharacterMaxDrawing()
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

#region CharacterAction
	public void MoveUserCharacter(Card card, Vector3[] route, System.Action callback)
	{
		StartCoroutine(ActionCharacter(userCharacter, card, true));
		LeanTween.moveSplineLocal(userCharacter.gameObject, route, 3f).setOnComplete(
			()=> {
				callback();
				userCharacter.RemoveCard();
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

	public void AddUserCharacter()
	{
		userCharacter = AddUserCharacter(User.GetUser());
		userCharacter.MoveTo(DEFAULT_USER_POSITION);
	}

	private UserCharacter AddUserCharacter(User data)
	{
		UserCharacter character = AddCharacter<UserCharacter>(data.sprite);
		character.Init(data, sortingOrder);
		sortingOrder++;
		return character;
	}

	public void AddMonster(List<Monster> monsterList)
	{
		int count = monsterList.Count;
		System.Random random = new System.Random();
		List<Vector2> positionList = DEFAULT_MONSTER_POSITION.OrderBy(x => random.Next()).Take(count).ToList();

		int index = 0;
		foreach (Monster monster in monsterList)
		{
			MonsterCharacter mc = AddMonster(monster);
			mc.MoveTo(positionList[index]);
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
