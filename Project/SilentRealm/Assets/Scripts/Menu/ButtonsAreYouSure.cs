using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsAreYouSure : MonoBehaviour {

	bool canSelect;
	UtilityLevelManager levelManager;
	public AudioSource select;
	public AudioSource delete;

	void Start()
	{
		levelManager = GameObject.Find("LevelManager").GetComponent<UtilityLevelManager>();
	}

	void Update ()
	{
		// check to see if we're not still pressing select so we don't immediately select something
		if (!Input.GetButton("Select"))
		{
			canSelect = true;
		}

		if (GetComponent<MenuNavigation>().isActive)
		{
			if (Input.GetButtonDown("Select") && canSelect)
			{
				select.Play();
				switch (GetComponent<MenuNavigation>().currentSelection)
				{
				case 1 :
					Debug.Log("yes I'm sure");
					GameObject.Find("LevelManager").GetComponent<UtilityLevelManager>().resetLevelData();
					delete.Play();
					GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive("Main Menu");
					GameObject.Find("Menu Control").GetComponent<MenuControl>().MoveCamera();
					break;
				case 0 :
					Debug.Log("just kidding");
					GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive("Main Menu");
					GameObject.Find("Menu Control").GetComponent<MenuControl>().MoveCamera();
					break;
				}
			}
		}
		else
		{
			canSelect = false;
		}
	}
}