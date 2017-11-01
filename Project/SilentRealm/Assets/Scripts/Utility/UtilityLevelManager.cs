using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UtilityLevelManager : MonoBehaviour {

	private string filePath = "Assets/Text/levelSaves.txt";
	public LevelLog[] levels;

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

	public void outputLevelData()
	{
		// write to the file using the array
		StreamWriter writer = new StreamWriter(filePath);

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
}

[System.Serializable]
public struct LevelLog
{
	public string name;
	public int isUnlocked;
	public int bestScore;
}