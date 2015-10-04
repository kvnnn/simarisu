using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewManager : GameMonoBehaviour
{
	protected MasterManager masterManager;

	public virtual void Init(MasterManager masterManager)
	{
		this.masterManager = masterManager;
	}

#region Show
	public IEnumerator Show()
	{
		BeforeShow();
		ShowView();
		OnShow();
		yield return new WaitForEndOfFrame();
		AfterShow();
	}

	private void ShowView()
	{
		gameObject.SetActive(true);
	}

	protected virtual void BeforeShow()
	{

	}

	protected virtual void OnShow()
	{

	}

	protected virtual void AfterShow()
	{

	}
#endregion

#region Hide
	public void Hide()
	{
		BeforeHide();
		HideView();
		OnHide();
		AfterHide();
	}

	private void HideView()
	{
		gameObject.SetActive(false);
	}

	protected virtual void BeforeHide()
	{

	}

	protected virtual void OnHide()
	{

	}

	protected virtual void AfterHide()
	{

	}
#endregion
}
