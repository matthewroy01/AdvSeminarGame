using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour {

	public Text info;
	private string currentText;
	private UtilityLevelManager levelManager;

	void Start ()
	{
		levelManager = GameObject.Find("LevelManager").GetComponent<UtilityLevelManager>();
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
		currentText = (GetComponent<MenuNavigation>().buttons[GetComponent<MenuNavigation>().currentSelection].name).ToString() + "\n" +
					  "Best Score: " +
					  levelManager.getBestScore(GetComponent<MenuNavigation>().buttons[GetComponent<MenuNavigation>().currentSelection].name).ToString();
		info.text = currentText;
	}
}
