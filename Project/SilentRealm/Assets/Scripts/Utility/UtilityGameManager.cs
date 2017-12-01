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
	public ParticleSystem exitParts;
	private bool partsExist;

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

	[Header("Pausing")]
	public bool paused = false;
	public Text txtPause;

	[Header("Sound Management")]
	public UtilityMusicManager MusicManager;
	public UtilityAudioManager FXManager;
	public AudioClip awaken;

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
		// over arching input
		CheckInput();

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
		if (keysInLevel <= keysCollected && partsExist == false)
        {
			Instantiate(exitParts, exit.transform.position, exit.transform.rotation);
			partsExist = true;
			txtKeys.color = new Color(0, 255, 0);
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

		MusicManager.Panic(state);

		if (state == true)
		{
			FXManager.PlaySound(awaken, 1.0f);
		}

		if (state == false && panicMode == true)
		{
			DestroyAllWebs();
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
		if (Input.GetButtonDown("Escape") && paused)
		{
			SceneManager.LoadScene(0);
		}

		if (Input.GetButtonDown("Pause"))
		{
			paused = !paused;
			txtPause.gameObject.SetActive(paused);
		}
	}

	public void StopAllMusic()
	{
		MusicManager.StopAll();
	}

	private void DestroyAllWebs()
	{
		GameObject[] webs;
		webs = GameObject.FindGameObjectsWithTag("Web");
		Debug.Log("UTILITYBROADCAST - Destroying " + webs.Length + " webs in scene.");
		for (int i = 0; i < webs.Length; i++)
		{
			Destroy(webs[i]);
		}
	}
}