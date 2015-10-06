using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameViewManager : ViewManager
{
	[SerializeField]
	private GameManager gameManager;

	[SerializeField]
	private Transform uiBaseTransform;
	[SerializeField]
	private GameObject cardListPartsPrefab;
	[SerializeField]
	private GameObject startBattleButtonPartsPrefab;

	protected override void BeforeShow()
	{
		gameManager.InitGame();
		InitUI();
	}

	protected override void AfterShow()
	{
		gameManager.StartGame();
	}

	private void InitUI()
	{
		CardListParts cardListParts = InstantiateUI<CardListParts>(cardListPartsPrefab);
		ButtonParts startBattleButtonParts = InstantiateUI<ButtonParts>(startBattleButtonPartsPrefab);
		gameManager.InitUI(cardListParts, startBattleButtonParts);
	}

	private T InstantiateUI<T>(GameObject prefab)
		where T : BaseUIParts
	{
		T uiParts = uiBaseTransform.GetComponentInChildren<T>();
		if (uiParts == null)
		{
			GameObject uiPartsGameObject = Instantiate(prefab);
			uiPartsGameObject.transform.SetParent(uiBaseTransform);
			uiParts = uiPartsGameObject.GetComponent<T>();
		}

		uiParts.Init();
		return uiParts;
	}
}
