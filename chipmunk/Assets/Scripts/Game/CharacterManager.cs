using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : GameMonoBehaviour
{
	[SerializeField]
	private GameManager gameManager;

	[SerializeField]
	private GameObject characterPrefab;

	private UserCharacter user;
	private List<MonsterCharacter> monsters = new List<MonsterCharacter>();

	public void Init()
	{
		DestroyAll();

		// For test
		var uc = AddCharacter<UserCharacter>(1);
		uc.MoveTo(gameManager.stageManager.GetCellPosition(1,1));
		var mc = AddCharacter<MonsterCharacter>(2);
		mc.MoveTo(gameManager.stageManager.GetCellPosition(4,1));
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
		if (user == null) {return;}
		user.DestroyIfExist();
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
