using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour {

	public Text info;
	private string currentText;
	private UtilityLevelManager levelManager;
	private MenuNavigation menuNavigation;
	public Text goBack;

	void Start ()
	{
		levelManager = GameObject.Find("LevelManager").GetComponent<UtilityLevelManager>();
		menuNavigation = GetComponent<MenuNavigation>();
	}

	void Update ()
	{
		if (menuNavigation.isActive == true)
		{
			// render text if the level select is active
			DoGUI();
			goBack.gameObject.SetActive(true);
		}
		else
		{
			// otherwise render nothing
			info.text = "";
			goBack.gameObject.SetActive(false);
		}
	}

	void DoGUI ()
	{
		currentText = (menuNavigation.buttons[menuNavigation.currentSelection].name).ToString() + "\n" +
					   "Best Score: " + levelManager.getBestScore(menuNavigation.buttons[menuNavigation.currentSelection].name).ToString();
		if (levelManager.getIsUnlocked(menuNavigation.buttons[menuNavigation.currentSelection].name) == 0)
		{
			currentText = currentText + "\nLOCKED";
		}
		info.text = currentText;


	}
}
