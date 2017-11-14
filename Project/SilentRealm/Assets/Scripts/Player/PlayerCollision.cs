using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : Player
{
	private Vector2 webVelocity;

	[Header("Visual FX")]
	public GameObject whiteFlash;
	public GameObject blackFade;

	[Header("Levels to unlock upon completing this one")]
	public string[] unlocks;

	private UtilityLevelManager levelManager;
	private Animator anim;

    void Start()
    {
		anim = gameObject.GetComponent<Animator>();

        // find the game manager
        FindGameManager();

		levelManager = GameObject.Find("LevelManager").GetComponent<UtilityLevelManager>();

		movementEnabled = true;
    }

	void Update()
	{
		UpdateStuck();

		anim.SetBool("movementEnabled", movementEnabled);

		// ignore collision with voids if we can't move
		if (movementEnabled == false)
		{
			Physics2D.IgnoreLayerCollision(gameObject.layer, 11, true);
		}
		else
		{
			Physics2D.IgnoreLayerCollision(gameObject.layer, 11, false);
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
    	// layer 12 is "DoppelOnly"
		if (other.CompareTag("Key") && other.gameObject.layer != 12)
        {
			// white flash effect
			if (getGameManager().panicMode == true)
			{
				Instantiate(whiteFlash, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation);
			}

            // increase the current number of keys
            getGameManager().keysCollected++;

			// stop panic mode
			getGameManager().Panic (false);

            // reset the player's velocity and position from panic mode
            PlayerReset(other);

            // destroy the key
            Destroy(other.gameObject);
        }

		// getting hit by an enemy
		if (other.gameObject.CompareTag("Enemy"))
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}

		// if you get hit by a web
		if (other.gameObject.CompareTag("Web"))
		{
			if (movementEnabled)
			{
				movementEnabled = false;
				webVelocity = other.GetComponent<Rigidbody2D>().velocity;
			}
			Destroy(other.gameObject);
		}

		// entering a spotlight's trigger
		if (other.gameObject.CompareTag("Spotlight") && getGameManager().panicMode == false)
		{
			getGameManager().Panic(true);
		}

		// exiting the level and winning
		if (other.gameObject.CompareTag("WinTrigger"))
		{
			movementEnabled = false;
			getGameManager().Panic(false);
			GetComponent<AudioSource>().Play();
			webVelocity = new Vector2(0,0);
			Instantiate(blackFade, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation);
			TellLevelManager();
			Invoke("Win", 4.0f);
		}
    }

    void Win()
    {
		SceneManager.LoadScene (0);
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		// hitting a wall while webbed and not in panic mode should snap the player back to the grid
		if (movementEnabled == false && getGameManager().panicMode == false && other.gameObject.layer == 8)
		{
			movementEnabled = true;
			SnapToGrid();
		}
		// otherwise, just reenable movement
		else if (movementEnabled == false && other.gameObject.layer == 8)
		{
			movementEnabled = true;
		}
	}

    private void PlayerReset(Collider2D other)
    {
        transform.position = other.transform.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

	private void UpdateStuck()
	{
		if (!movementEnabled)
		{
			GetComponent<Rigidbody2D>().velocity = webVelocity;
		}
	}

	private void TellLevelManager()
	{
		if (GameObject.Find("LevelManager") != null)
		{
			levelManager.updateBestScore(SceneManager.GetActiveScene().name, getGameManager().steps);
			for (int i = 0; i < unlocks.Length; i++)
			{
				levelManager.unlock(unlocks[i]);
			}
		}
	}

	private void SnapToGrid()
	{
		int tmpx = (int)transform.position.x, tmpy = (int)transform.position.y;
		Debug.Log("( " + tmpx + ", " + tmpy + " )");

		// snap to the nearest whole number
		if (transform.position.x > tmpx + 0.5)
		{
			transform.position = new Vector2(tmpx + 1, transform.position.y);
		}
		else
		{
			transform.position = new Vector2(tmpx, transform.position.y);
		}

		if (transform.position.y > tmpy + 0.5)
		{
			transform.position = new Vector2(transform.position.x, tmpy + 1);
		}
		else
		{
			transform.position = new Vector2(transform.position.x, tmpy);
		}
	}
}