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
	public GameObject deathFade;
	public ParticleSystem keyParts;

	[Header("Levels to unlock upon completing this one")]
	public string[] unlocks;

	private UtilityLevelManager levelManager;
	private Animator anim;

	[Header("Sound")]
	public AudioClip collectAll;
	public AudioClip keyHigh;
	public AudioClip keyLow;
	public AudioClip death;

    void Start()
    {
		anim = gameObject.GetComponent<Animator>();

        // find the game manager
        FindGameManager();

		levelManager = GameObject.Find("LevelManager").GetComponent<UtilityLevelManager>();

		webbed = false;
		dead = false;
    }

	void Update()
	{
		UpdateStuck();

		anim.SetBool("webbed", webbed);
		anim.SetBool("dead", dead);

		// ignore collision with voids if we can't move
		if (webbed == true)
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
				// play a sound specific to exiting panic mode
				getGameManager().FXManager.PlaySound(keyLow, 0.5f);

				// make the screen flash white
				Instantiate(whiteFlash, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation);
			}
			else
			{
				// play a sound for picking up a key
				getGameManager().FXManager.PlaySound(keyHigh, 0.25f);
			}

            // increase the current number of keys
            getGameManager().keysCollected++;

			// stop panic mode
			getGameManager().Panic (false);

            // reset the player's velocity and position from panic mode
            PlayerReset(other);

            // destroy the key
            Destroy(other.gameObject);

			// instantiate particles
			Instantiate(keyParts, transform.position, transform.rotation);
        }

		// getting hit by an enemy
		if (other.gameObject.CompareTag("Enemy"))
		{
			Kill();
		}

		// if you get hit by a web
		if (other.gameObject.CompareTag("Web"))
		{
			if (!webbed)
			{
				webbed = true;
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
			webbed = false;
			winState = true;
			getGameManager().Panic(false);
			getGameManager().FXManager.PlaySound(collectAll, 1.0f);
			getGameManager().StopAllMusic();
			webVelocity = new Vector2(0,0);
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			transform.position = other.transform.position;
			Instantiate(blackFade, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation);
			TellLevelManager();
			Invoke("Win", 4.0f);
		}
    }

    void Win()
    {
		winState = false;
		// write to the file
		levelManager.outputLevelData();
		SceneManager.LoadScene (0);
    }

	public void Kill()
	{
		if (dead == false)
		{
			getGameManager().StopAllMusic();
			getGameManager().FXManager.PlaySound(death, 1.0f);
			webbed = false;
			dead = true;
			webVelocity = new Vector2(0,0);
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			transform.position = new Vector3(transform.position.x, transform.position.y, -6);
			Invoke("Fade", 1.0f);
			Invoke("Reload", 1.3f);
		}
	}


	private void Fade () { Instantiate(deathFade, new Vector3(transform.position.x, transform.position.y, -5), transform.rotation); }
	private void Reload () { SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex); }

	void OnCollisionEnter2D(Collision2D other)
	{
		// hitting a wall while webbed and not in panic mode should snap the player back to the grid
		if (webbed == true && getGameManager().panicMode == false && other.gameObject.layer == 8)
		{
			webbed = false;
			SnapToGrid();
		}
		// otherwise, just reenable movement
		else if (webbed == true && other.gameObject.layer == 8)
		{
			webbed = false;
		}
	}
	void OnCollisionStay2D(Collision2D other)
	{
		// hitting a wall while webbed and not in panic mode should snap the player back to the grid
		if (webbed == true && getGameManager().panicMode == false && other.gameObject.layer == 8)
		{
			webbed = false;
			SnapToGrid();
		}
		// otherwise, just reenable movement
		else if (webbed == true && other.gameObject.layer == 8)
		{
			webbed = false;
		}
	}

    private void PlayerReset(Collider2D other)
    {
        transform.position = other.transform.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

	private void UpdateStuck()
	{
		// move according to the webVelocity
		if (webbed && getGameManager().paused == false)
		{
			GetComponent<Rigidbody2D>().velocity = webVelocity;
		}
		// else, if we're paused, stop moving
		else if (getGameManager().paused == true)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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