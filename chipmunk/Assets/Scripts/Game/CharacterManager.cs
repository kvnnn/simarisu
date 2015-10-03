using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : GameMonoBehaviour
{
	[SerializeField]
	private GameObject characterPrefab;

	private UserCharacter user;
	private List<MonsterCharacter> monsters = new List<MonsterCharacter>();

	public void Init()
	{
		DestroyAll();

		// For test
		AddCharacter<UserCharacter>(1);
		AddCharacter<MonsterCharacter>(2);
	}

	private void AddCharacter<T>(int characterId)
		where T : BaseCharacter
	{
		GameObject characterGameObject = Instantiate(characterPrefab);
		characterGameObject.transform.SetParent(transform);

		T character = characterGameObject.AddComponent<T>();
		character.Init(GetSprite(characterId));
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
