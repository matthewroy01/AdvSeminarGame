using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityBroadcast : MonoBehaviour {

	public GameObject[] enemies;
	// I found this thing about Delegates but as far as I know, it's only useful across a single script.
	// delegate void DelegateEnemyAI();
	// https://unity3d.com/learn/tutorials/topics/scripting/delegates

	void Start () {
		// find all enemies
		FindAllEnemies();


	}

	void Update () {
		// set the target framerate
		Application.targetFrameRate = 30;
	}

	private void FindAllEnemies()
	{
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		Debug.Log("UTILITYBROADCAST - Found " + enemies.Length + " enemies in scene.");
	}

	public void togetherNow()
	{
		// Creating an actual event system would be really useful here.
		// Unity's event system doesn't seem very user friendly for anything but UI.
		// Every enemy will have their own AI script so it would be useful if they just listened
			// for an event so that they can call their respective AI function.
		for (int i = 0; i < enemies.Length; i++)
		{
			enemies[i].GetComponent<Enemy>().Step();
		}
	}
}
