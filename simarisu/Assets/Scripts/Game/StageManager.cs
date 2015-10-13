using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StageManager : GameMonoBehaviour
{
	private int stageId;
	private GameObject stageGameObject;

	private const string STAGE_RESOURCE_PATH = "Prefabs/Stages/";

	public void Init()
	{
		DestoryIfExist();
	}

	public void LoadStage(int stageId)
	{
		if (stageGameObject != null && this.stageId == stageId) {return;}
		this.stageId = stageId;

		stageGameObject = Instantiate(Resources.Load<GameObject>(STAGE_RESOURCE_PATH + stageId));
		stageGameObject.transform.SetParent(transform);
		stageGameObject.transform.localPosition = Vector3.zero;
	}

	private void DestoryIfExist()
	{
		if (stageGameObject != null)
		{
			Destroy(stageGameObject);
		}
	}
}
