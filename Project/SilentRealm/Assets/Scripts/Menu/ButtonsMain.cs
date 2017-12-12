using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsMain : MonoBehaviour {

	bool canSelect;
	public AudioSource select;

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
					case 0 :
						Debug.Log("going to level select");
						GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive("Level Select");
						GameObject.Find("Menu Control").GetComponent<MenuControl>().MoveCamera();
						break;

					case 1 :
						Debug.Log("going to controls screen");
						GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive("Controls");
						GameObject.Find("Menu Control").GetComponent<MenuControl>().MoveCamera();
						break;

					case 2 :
						Debug.Log("are you sure you want to do that?");
						GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive("AreYouSure");
						GameObject.Find("Menu Control").GetComponent<MenuControl>().MoveCamera();
						break; 

					case 3 :
						Application.Quit();
						Debug.Log("quitting the game");
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