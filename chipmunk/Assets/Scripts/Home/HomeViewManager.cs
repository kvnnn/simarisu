using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeViewManager : ViewManager
{

	public void StartBtnClick()
	{
		masterManager.ChangeView(MasterManager.View.Game);
	}
}
