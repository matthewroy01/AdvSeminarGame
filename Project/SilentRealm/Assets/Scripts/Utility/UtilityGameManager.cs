using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtilityGameManager : MonoBehaviour {

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
	public Text txtWarning;

	[Header("Animation Management")]
	public bool checkingAnimations;

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
		// check to see how many keys have been collected
        CheckKeys();

		// update the UI that isn't possible in the OnGUI function
		UpdateGUI();
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
		checkingAnimations = true;
	}

    // tells all objects to panic or stop panicing
    public void Panic(bool state)
    {
		Debug.Log ("BROADCAST - setting panic state to " + state);

		if (state == true)
		{
			txtWarning.gameObject.SetActive(true);
			txtWarning.material.color = new Color(txtWarning.material.color.r, txtWarning.material.color.g, txtWarning.material.color.b, 1);
		}

		for (int i = 0; i < enemies.Length; i++)
		{
			enemies [i].GetComponent<Enemy>().Panic(state);
		}
		player.GetComponent<PlayerMovement>().Panic(state);
    }

    private void OnGUI()
    {
		// update total keys in the UI
		txtKeys.text = "Keys: " + keysCollected + "/" + keysInLevel;
    }

	private void UpdateGUI()
	{
		

		// make Panic text fade if the text appears
		if (txtWarning.material.color.a > 0)
		{
			txtWarning.material.color = new Color(txtWarning.material.color.r, txtWarning.material.color.g, txtWarning.material.color.b, txtWarning.material.color.a - 0.05f);
		}
	}
}
