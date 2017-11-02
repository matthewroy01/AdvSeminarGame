using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour {

	public Text info;
	private string currentText;
	private UtilityLevelManager levelManager;
	private MenuNavigation menuNavigation;

	void Start ()
	{
		levelManager = GameObject.Find("LevelManager").GetComponent<UtilityLevelManager>();
		menuNavigation = GetComponent<MenuNavigation>();
	}

	void Update ()
	{
		if (GetComponent<MenuNavigation>().isActive == true)
		{
			DoGUI();
		}
	}

	void DoGUI ()
	{
		currentText = (menuNavigation.buttons[menuNavigation.currentSelection].name).ToString() + "\n" +
					   "Best Score: " + levelManager.getBestScore(menuNavigation.buttons[menuNavigation.currentSelection].name).ToString();
		info.text = currentText;
	}
}
