using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class UtilityLevelManager : MonoBehaviour {

	private string filePath = "Assets/Text/levelSaves.txt";
	private string defaultPath = "Assets/Text/defaultSaves.txt";

	public int unlockedSomething = -1;

	// for using playerprefs, written by Neil
	/*[Serializable]
	public class LevelSave
	{
		public LevelLog[] levels;

	}*/

	public LevelLog[] levels;

	[Header("For high score display")]
	public Sprite congrats;

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

		// read from file
		loadLevelData();
	}

	void Update()
	{
		if (unlockedSomething != -1 && SceneManager.GetActiveScene().name == "Main Menu")
		{
			GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive("Level Select");
			GameObject.Find("Menu Control").GetComponent<MenuControl>().MoveCamera();
			unlockedSomething = -1;
		}
	}

	void loadLevelData()
	{
		// read from the file and populate the array
		StreamReader reader = new StreamReader(filePath);

		for (int i = 0; i < levels.Length; i++)
		{
			levels[i].name = reader.ReadLine();
			// int.Parse converts string to int
			levels[i].isUnlocked = int.Parse(reader.ReadLine());
			levels[i].bestScore = int.Parse(reader.ReadLine());
		}

		reader.Close();
	}

	public void resetLevelData()
	{
		// read from the file and populate the array
		StreamReader reader = new StreamReader(defaultPath);
		StreamWriter writer = new StreamWriter(filePath);

		string tmp = "";

		tmp = reader.ReadToEnd();

		writer.Write(tmp);

		reader.Close();
		writer.Close();

		loadLevelData();
	}

	public void outputLevelData()
	{
		// write to the file using the array
		StreamWriter writer = new StreamWriter(filePath);
		// for using playerprefs, written by Neil
		/* JsonUtility.ToJson(LevelSave); */

		for (int i = 0; i < levels.Length; i++)
		{
			writer.WriteLine(levels[i].name);
			writer.WriteLine(levels[i].isUnlocked.ToString());
			writer.WriteLine(levels[i].bestScore.ToString());
		}

		writer.Close();
	}

	public int getIsUnlocked(string name)
	{
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i].name == name)
			{
				return levels[i].isUnlocked;
			}
		}
		Debug.Log(name + "is not a valid level name.");
		return -1;
	}

	public int getBestScore(string name)
	{
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i].name == name)
			{
				return levels[i].bestScore;
			}
		}
		Debug.Log(name + "is not a valid level name.");
		return 99999;
	}

	public void updateBestScore(string name, int newScore)
	{
		int i;
		// find the level we're looking for
		for (i = 0; i < levels.Length; i++)
		{
			if (levels[i].name == name)
			{
				break;
			}
		}

		if (i == levels.Length)
		{
			Debug.Log(name + " was not found when updating scores");
			return;
		}

		// check the score and update accordingly
		if (levels[i].bestScore > newScore)
		{
			levels[i].bestScore = newScore;
		}
	}

	public void unlock(string name)
	{
		for (int i = 0; i < levels.Length; i++)
		{
			if (levels[i].name == name)
			{
				levels[i].isUnlocked = 1;
				unlockedSomething = i;
				return;
			}
		}
		Debug.Log(name + " is not a valid level name.");
	}
}

[System.Serializable]
public struct LevelLog
{
	public string name;
	public int isUnlocked;
	public int bestScore;
}