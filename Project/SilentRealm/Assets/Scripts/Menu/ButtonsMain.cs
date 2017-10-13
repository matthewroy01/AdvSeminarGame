using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsMain : MonoBehaviour {

	void Update ()
	{
		if (GetComponent<MenuNavigation>().isActive)
		{
			if (Input.GetButtonDown("Select"))
			{
				switch (GetComponent<MenuNavigation>().currentSelection)
				{
					case 0 :
						Debug.Log("going to level select");
						GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive("Level Select");
						break;

					case 1 :
						Debug.Log("quitting the game");
						break;
				}
			}
		}
	}
}