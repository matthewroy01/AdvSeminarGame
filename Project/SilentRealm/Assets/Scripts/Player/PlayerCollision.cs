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
    }

	void Update()
	{
		UpdateStuck();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.CompareTag("Key"))
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
			SceneManager.LoadScene (0);
		}

		if (other.gameObject.CompareTag("Web"))
		{
			movementEnabled = false;
			//GetComponent<PlayerMovement>().movementEnabled = false;
			webVelocity = other.GetComponent<Rigidbody2D>().velocity;
			Destroy(other.gameObject);
		}
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer == 8)
		{
			movementEnabled = true;
			//GetComponent<PlayerMovement>().movementEnabled = true;
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