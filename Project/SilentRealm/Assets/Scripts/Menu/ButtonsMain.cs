using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsMain : MonoBehaviour {

	public AudioSource select;

	void Update ()
	{
		if (GetComponent<MenuNavigation>().isActive)
		{
			if (Input.GetButtonDown("Select"))
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
						GameObject.Find("LevelManager").GetComponent<UtilityLevelManager>().outputLevelData();
						Application.Quit();
						Debug.Log("quitting the game");
						break;
				}
			}
		}
	}
}