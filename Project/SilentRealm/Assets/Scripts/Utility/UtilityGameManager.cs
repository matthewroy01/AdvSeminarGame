using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Text txtSteps;
	public GameObject txtWarning;
	private bool fadeAway = false;

	private bool checkingAnimations = false;

	public int steps = 0;

	void Start () {
		// find all enemies
		FindAllEnemies();

        // find the player
        FindThePlayer();

        // find keys
        FindAllKeys();

        // find the exit
        FindTheExit();

		// set warning text's default transparency
		txtWarning.GetComponent<SpriteRenderer>().color = new Color(txtWarning.GetComponent<SpriteRenderer>().color.r,
																	txtWarning.GetComponent<SpriteRenderer>().color.g,
																	txtWarning.GetComponent<SpriteRenderer>().color.b,
																	0);
	}

    void Update()
    {
		// check to see how many keys have been collected
        CheckKeys();

		// update the UI that isn't possible in the OnGUI function
		UpdateGUI();

		// over arching input
		CheckInput();
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
		steps++;
	}

	public bool DoneWithAnimations()
	{
		if (checkingAnimations == true)
		{
			for (int i = 0; i < enemies.Length; i++)
			{
				if (enemies[i].GetComponent<God>().GetAnimationState() == false)
				{
					return false;
				}
			}
			if (player.GetComponent<God>().GetAnimationState() == false)
			{
				return false;
			}
			Debug.Log ("BROADCAST - all animations completed");
			checkingAnimations = false;
			return true;
		}

		return true;
	}

    // tells all objects to panic or stop panicing
    public void Panic(bool state)
    {
		Debug.Log ("BROADCAST - setting panic state to " + state);

		fadeAway = state;

		player.GetComponent<PlayerMovement>().Panic(state);

		for (int i = 0; i < enemies.Length; i++)
		{
			enemies [i].GetComponent<Enemy>().Panic(state);
		}

		panicMode = state;
    }

    private void OnGUI()
    {
		// update total keys in the UI
		txtKeys.text = "Keys: " + keysCollected + "/" + keysInLevel;
		txtSteps.text = "Steps: " + steps;
    }

	private void UpdateGUI()
	{
		// enable fade text
		if (fadeAway)
		{
			txtWarning.GetComponent<SpriteRenderer>().color = new Color(txtWarning.GetComponent<SpriteRenderer>().color.r,
																		txtWarning.GetComponent<SpriteRenderer>().color.g,
																		txtWarning.GetComponent<SpriteRenderer>().color.b,
																		1);
			fadeAway = false;
		}

		// text fades away over time
		if (txtWarning.GetComponent<SpriteRenderer>().color.a > 0)
		{
			txtWarning.GetComponent<SpriteRenderer>().color = new Color(txtWarning.GetComponent<SpriteRenderer>().color.r,
																		txtWarning.GetComponent<SpriteRenderer>().color.g,
																		txtWarning.GetComponent<SpriteRenderer>().color.b,
																		txtWarning.GetComponent<SpriteRenderer>().color.a - 0.01f);
		}
	}

	private void CheckInput()
	{
		if (Input.GetButtonDown("Escape"))
		{
			SceneManager.LoadScene(0);
		}
	}
}