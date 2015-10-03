using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterManager : GameMonoBehaviour
{
	void Awake()
	{
		// Debug
		// PlayerPrefs.DeleteAll();

		Time.timeScale = 1f;
	}

	void Start()
	{

	}

	void OnApplicationQuit()
	{
		// PlayerPrefs.Flush();
	}
}
