using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterManager : GameMonoBehaviour
{
	[SerializeField]
	private Transform viewBaseTransform;

	[SerializeField]
	private List<GameObject> viewPrefabs;
	private ViewManager currentViewManager;
	private View currentView;
	private int currentViewId
	{
		get {return (int)currentView;}
	}
	private string currentViewStr
	{
		get {return currentView.ToString();}
	}
	public enum View
	{
		Home = 0,
		Game = 1,
	}

	void Awake()
	{
		Time.timeScale = 1f;
	}

	void Start()
	{
		currentView = View.Home;
		ChangeView();
	}

	void OnApplicationQuit()
	{
	}

	public void ChangeView(View view)
	{
		if (currentView == view) {return;}
		currentView = view;
		ChangeView();
	}

	private void ChangeView()
	{
		GameObject viewGameObject = null;
		Transform viewTransform = viewBaseTransform.Find(currentViewStr);
		viewGameObject = (viewTransform != null) ? viewTransform.gameObject : ImportView(currentView);

		HideCurrentView();
		currentViewManager = viewGameObject.GetComponent<ViewManager>();
		ShowCurrentView();
	}

	private void HideCurrentView()
	{
		if (currentViewManager == null) {return;}
		currentViewManager.Hide();
	}

	private void ShowCurrentView()
	{
		StartCoroutine(currentViewManager.Show());
	}

	private GameObject ImportView(View view)
	{
		int viewId = (int)view;
		string viewStr = view.ToString();

		GameObject viewGameObject = Instantiate(viewPrefabs[viewId]);
		viewGameObject.transform.SetParent(viewBaseTransform);
		viewGameObject.name = viewStr;

		ViewManager viewManager = viewGameObject.GetComponent<ViewManager>();
		viewManager.Init(this);

		return viewGameObject;
	}
}
