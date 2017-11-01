using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : Player
{
	//public GameObject stuck = null;

	public Vector2 webVelocity;

    void Start()
    {
        // find the game manager
        FindGameManager();

		movementEnabled = true;
    }

	void Update()
	{
		UpdateStuck();

		if (movementEnabled == false)
		{
			Physics2D.IgnoreLayerCollision(gameObject.layer, 11);
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
    	// layer 12 is "DoppelOnly"
		if (other.CompareTag("Key") && other.gameObject.layer != 12)
        {
            // increase the current number of keys
            getGameManager().keysCollected++;

			// stop panic mode
			getGameManager().Panic (false);

            // reset the player's velocity and position from panic mode
            PlayerReset(other);

            // destroy the key
            Destroy(other.gameObject);
        }

		if (other.gameObject.CompareTag("Enemy"))
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}

		if (other.gameObject.CompareTag("Web"))
		{
			if (movementEnabled)
			{
				movementEnabled = false;
			}
			//GetComponent<PlayerMovement>().movementEnabled = false;
			webVelocity = other.GetComponent<Rigidbody2D>().velocity;
			Destroy(other.gameObject);
		}

		if (other.gameObject.CompareTag("Spotlight") && GetComponent<PlayerMovement>().panicMode == false)
		{
			getGameManager().Panic(true);
		}

		if (other.gameObject.CompareTag("WinTrigger"))
		{
			GetComponent<PlayerVisuals>().fadeToBlack = true;
			movementEnabled = false;
			getGameManager().Panic(false);
			GetComponent<AudioSource>().Play();
			webVelocity = new Vector2(0,0);
			if (GameObject.Find("LevelManager") != null)
			{
				GameObject.Find("LevelManager").GetComponent<UtilityLevelManager>().updateBestScore(SceneManager.GetActiveScene().name, getGameManager().steps);
			}
			Invoke("Win", 4.0f);
		}
    }

    void Win()
    {
		SceneManager.LoadScene (0);
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		if (movementEnabled == false && getGameManager().panicMode == false && other.gameObject.layer == 8)
		{
			movementEnabled = true;

			int tmpx = (int)transform.position.x, tmpy = (int)transform.position.y;
			Debug.Log("( " + tmpx + ", " + tmpy + " )");

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
}