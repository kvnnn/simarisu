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
	private GameObject chipListPartsPrefab;

	protected override void BeforeShow()
	{
		GameStartCoroutine(gameManager.InitGame());
		InitUI();
	}

	protected override void AfterShow()
	{
		gameManager.StartGame();
	}

	private void InitUI()
	{
		ChipListParts chipListParts = InstantiateUI<ChipListParts>(chipListPartsPrefab);
		gameManager.InitUI(chipListParts);
	}

	private T InstantiateUI<T>(GameObject prefab)
		where T : BaseUIParts
	{
		T uiParts = null;
		if (!gameManager.isUIPartsInstantiated) {
			GameObject uiPartsGameObject = Instantiate(prefab);
			uiPartsGameObject.transform.SetParent(uiBaseTransform);
			uiParts = uiPartsGameObject.GetComponent<T>();
		} else {
			uiParts = uiBaseTransform.GetComponentInChildren<T>();
		}

		uiParts.Init();
		return uiParts;
	}
}
