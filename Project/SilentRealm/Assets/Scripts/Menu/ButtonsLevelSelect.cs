using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsLevelSelect : MonoBehaviour {

	bool canSelect;

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
						Debug.Log("level 5");
						SceneManager.LoadScene(5);
						break;

					case 1 :
						Debug.Log("level 4");
						SceneManager.LoadScene(4);
						break;

					case 2 :
						Debug.Log("level 3");
						SceneManager.LoadScene(3);
						break;

					case 3 :
						Debug.Log("level 2");
						SceneManager.LoadScene(2);
						break;

					case 4 :
						Debug.Log("level 1");
						SceneManager.LoadScene(1);
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