using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsLevelSelect : MonoBehaviour {

	bool canSelect;
	UtilityLevelManager levelManager;

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
				switch (GetComponent<MenuNavigation>().currentSelection)
				{
					case 0 :
						Debug.Log("level 9");
						if (levelManager.getIsUnlocked("Level 9") == 1)
						{
							SceneManager.LoadScene(9);
						}
						else
						{
							Debug.Log("level locked");
						}
						break;
					case 1 :
						Debug.Log("level 8");
						if (levelManager.getIsUnlocked("Level 8") == 1)
						{
							SceneManager.LoadScene(8);
						}
						else
						{
							Debug.Log("level locked");
						}
						break;
					case 2 :
						Debug.Log("level 7");
						if (levelManager.getIsUnlocked("Level 7") == 1)
						{
							SceneManager.LoadScene(7);
						}
						else
						{
							Debug.Log("level locked");
						}
						break;
					case 3 :
						Debug.Log("level 6");
						if (levelManager.getIsUnlocked("Level 6") == 1)
						{
							SceneManager.LoadScene(6);
						}
						else
						{
							Debug.Log("level locked");
						}
						break;
					case 4 :
						Debug.Log("level 5");
						if (levelManager.getIsUnlocked("Level 5") == 1)
						{
							SceneManager.LoadScene(5);
						}
						else
						{
							Debug.Log("level locked");
						}
						break;

					case 5 :
						Debug.Log("level 4");
						if (levelManager.getIsUnlocked("Level 4") == 1)
						{
							SceneManager.LoadScene(4);
						}
						else
						{
							Debug.Log("level locked");
						}
						break;

					case 6 :
						Debug.Log("level 3");
						if (levelManager.getIsUnlocked("Level 3") == 1)
						{
							SceneManager.LoadScene(3);
						}
						else
						{
							Debug.Log("level locked");
						}
						break;

					case 7 :
						Debug.Log("level 2");
						if (levelManager.getIsUnlocked("Level 2") == 1)
						{
							SceneManager.LoadScene(2);
						}
						else
						{
							Debug.Log("level locked");
						}
						break;

					case 8 :
						Debug.Log("level 1");
						if (levelManager.getIsUnlocked("Level 1") == 1)
						{
							SceneManager.LoadScene(1);
						}
						else
						{
							Debug.Log("level locked");
						}
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