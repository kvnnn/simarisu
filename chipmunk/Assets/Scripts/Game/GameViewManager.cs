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
	[SerializeField]
	private GameObject chipSeletPartsPrefab;

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
		ChipListParts chipListParts = InstantiateUI<ChipListParts>(chipListPartsPrefab, gameManager.chipListParts != null);
		ChipSelectParts chipSelectParts = InstantiateUI<ChipSelectParts>(chipSeletPartsPrefab, gameManager.chipSelectParts != null);
		gameManager.InitUI(chipListParts, chipSelectParts);
	}

	private T InstantiateUI<T>(GameObject prefab, bool isInstantiated)
		where T : BaseUIParts
	{
		T uiParts = null;
		if (!isInstantiated) {
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
