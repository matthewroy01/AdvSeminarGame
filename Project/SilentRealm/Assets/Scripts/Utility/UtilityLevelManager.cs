using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class UtilityLevelManager : MonoBehaviour
{
	const int NUMBER_OF_LEVELS = 11;

	public int unlockedSomething = -1;

	[Header("For high score display")]
	public GameObject congrats;

	void Awake()
	{
		// if there is more than one levelManager, delete the new one
		GameObject tmp;
		tmp = GameObject.Find("LevelManager");
		if (tmp != gameObject)
		{
			Destroy(gameObject);
		}

		// persist between scenes
		Object.DontDestroyOnLoad(gameObject);

		congrats.SetActive(false);

		initializeData();
	}

	void Update()
	{
		if (unlockedSomething != -1 && SceneManager.GetActiveScene().name == "Main Menu")
		{
			GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive("Level Select");
			GameObject.Find("Menu Control").GetComponent<MenuControl>().MoveCamera();
			unlockedSomething = -1;
			congrats.SetActive(false);
		}
	}

	public void resetLevelData()
	{
		PlayerPrefs.DeleteAll();
		initializeData();
	}

	public int getIsUnlocked(string name)
	{
		return PlayerPrefs.GetInt(name + "U");
	}

	public int getBestScore(string name)
	{
		return PlayerPrefs.GetInt(name + "S");
	}

	public void updateBestScore(string name, int newScore)
	{
		if (newScore < PlayerPrefs.GetInt(name + "S"))
		{
			PlayerPrefs.SetInt(name + "S", newScore);

			Invoke("displayCongrats", 1.0f);
		}
	}

	void displayCongrats()
	{
		GameObject tmpPlayer = GameObject.Find("Player");
		congrats.transform.position = new Vector3(tmpPlayer.transform.position.x, tmpPlayer.transform.position.y - 2, -5);
		congrats.SetActive(true);
	}

	public void unlock(string name)
	{
		PlayerPrefs.SetInt("Level " + name + "U", 1);
		unlockedSomething = int.Parse(name);
	}

	void initializeData()
	{
		// check to see whether or not we need to initialize first
		if (!PlayerPrefs.HasKey("Level 1U"))
		{
			for (int i = 1; i <= NUMBER_OF_LEVELS; i++)
			{
				if (i == 1)
				{
					// level 1 should be unlocked by default
					PlayerPrefs.SetInt("Level " + i + "U", 1);
				}
				else
				{
					// lock all other levels
					PlayerPrefs.SetInt("Level " + i + "U", 0);
				}

				// initialize all scores as 99999
				PlayerPrefs.SetInt("Level " + i + "S", 99999);
			}
		}
	}
}

[System.Serializable]
public struct LevelLog
{
	public string name;
	public int isUnlocked;
	public int bestScore;
}