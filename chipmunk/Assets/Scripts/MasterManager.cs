using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterManager : GameMonoBehaviour
{
	[SerializeField]
	private Transform viewBaseTransform;

	[SerializeField]
	private List<GameObject> viewPrefabs;
	private View currentView;
	private int currentViewId {
		get {return (int)currentView;}
	}
	private string currentViewStr {
		get {return currentView.ToString();}
	}
	public enum View {
		Home = 0,
	}

	void Awake()
	{
		// Debug
		// PlayerPrefs.DeleteAll();

		Time.timeScale = 1f;
	}

	void Start()
	{
		currentView = View.Home;
		ChangeView();
	}

	void OnApplicationQuit()
	{
		// PlayerPrefs.Flush();
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
	}

	private GameObject ImportView(View view)
	{
		int viewId = (int)view;
		string viewStr = view.ToString();

		GameObject viewGameObject = Instantiate(viewPrefabs[viewId]);
		viewGameObject.transform.parent = viewBaseTransform;
		viewGameObject.name = viewStr;
		return viewGameObject;
	}
}
