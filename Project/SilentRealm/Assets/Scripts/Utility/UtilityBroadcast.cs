using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityBroadcast : MonoBehaviour {

	public GameObject[] enemies;
    public GameObject player;

    public bool panicMode = false;

	void Start () {
		// find all enemies
		FindAllEnemies();

        // find the player
        FindThePlayer();

        //StartCoroutine(Test(1));
	}

	void Update ()
    {
        if (panicMode)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().Step();
            }
        }
    }

	private void FindAllEnemies()
	{
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		Debug.Log("UTILITYBROADCAST - Found " + enemies.Length + " enemies in scene.");
	}

    private void FindThePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

	public void togetherNow()
	{
        // this is kinda hacky...
        if (!panicMode)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().StartCoroutine("Step");
            }
        }
	}

    public void moveOnYourOwnTime()
    {
        if (panicMode)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().StepOwn();
            }
        }
    }

    public void panic(bool state)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<God>().panicMode = state;
        }
        player.GetComponent<God>().panicMode = state;
    }

    //public IEnumerator Test(float time)
    //{
    //    while (true)
    //    {
    //        Debug.Log("test: " + Time.deltaTime);
    //        yield return new WaitForSeconds(time);
    //    }
    //}
}
