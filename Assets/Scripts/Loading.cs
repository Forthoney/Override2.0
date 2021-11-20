using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Loading : MonoBehaviour
{
	public string FirstToLoad = "B1";
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		try {
			if (FMODUnity.RuntimeManager.HasBanksLoaded)
			{
				Debug.Log("Master Bank Loaded");
				SceneManager.LoadScene(FirstToLoad, LoadSceneMode.Single);
			} else {
				Debug.Log("Master Bank Not Yet Loaded " + FMODUnity.RuntimeManager.AnyBankLoading());
			}
		} catch (Exception err) {
			Debug.Log(err);
		}

	}
}