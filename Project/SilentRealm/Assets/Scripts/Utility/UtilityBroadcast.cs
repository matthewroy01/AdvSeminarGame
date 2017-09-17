using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtilityBroadcast : MonoBehaviour {

    [Header("The player, enemies, and exit")]
	public GameObject[] enemies;
    public GameObject player;
    public GameObject exit;

    [Header("Panic mode master bool")]
    public bool panicMode = false;

    [Header("Key numbers")]
    public int keysCollected;
    public int keysInLevel;

    [Header("UI")]
    public Text txtKeys;

	void Start () {
		// find all enemies
		FindAllEnemies();

        // find the player
        FindThePlayer();

        // find keys
        FindAllKeys();

        // find the exit
        FindTheExit();
	}

    void Update()
    {
        CheckKeys();
    }

	private void FindAllEnemies()
	{
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		Debug.Log("UTILITYBROADCAST - Found " + enemies.Length + " enemies in scene.");
	}

    private void FindThePlayer() { player = GameObject.FindGameObjectWithTag("Player"); }

    private void FindAllKeys()
    {
        GameObject[] keys;
        keys = GameObject.FindGameObjectsWithTag("Key");
        Debug.Log("UTILITYBROADCAST - Found " + keys.Length + " keys in scene.");
        keysInLevel = keys.Length;
    }

    private void FindTheExit() { exit = GameObject.FindGameObjectWithTag("Exit"); }

    private void CheckKeys()
    {
        if (keysInLevel <= keysCollected)
        {
            exit.SetActive(false);
        }
    }

    // has all enemies move together
    public void togetherNow()
	{
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().StartCoroutine("Step");
        }
	}

    // tells all objects to panic or stop panicing
    public void panic(bool state)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<God>().panicMode = state;
        }
        player.GetComponent<God>().panicMode = state;
    }

    private void OnGUI()
    {
        txtKeys.text = "Keys: " + keysCollected + "/" + keysInLevel;
    }
}
