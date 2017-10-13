using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsLevelSelect : MonoBehaviour {

	void Update () {
		if (GetComponent<MenuNavigation>().isActive)
		{
			if (Input.GetButtonDown("Select"))
			{
				switch (GetComponent<MenuNavigation>().currentSelection)
				{
					case 0 :
						Debug.Log("level 4");
						break;

					case 1 :
						Debug.Log("level 3");
						break;

					case 2 :
						Debug.Log("level 2");
						SceneManager.LoadScene(2);
						break;

					case 3 :
						Debug.Log("level 1");
						SceneManager.LoadScene(1);
						break;
				}
			}
		}
	}
}
