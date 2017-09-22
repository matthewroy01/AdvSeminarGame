using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : God
{
	public GameObject stuck = null;

    void Start()
    {
        // find the game manager
        FindGameManager();

		stuck = null;
    }

	void Update()
	{
		UpdateStuck();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Key")
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

		if (other.gameObject.tag == "Enemy")
		{
			SceneManager.LoadScene (0);
		}

		if (other.gameObject.tag == "Web")
		{
			movementEnabled = false;
			if (stuck == null)
			{
				stuck = other.gameObject;
			}
		}
    }

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.layer == 8)
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
			GetComponent<Rigidbody2D>().velocity = stuck.GetComponent<Rigidbody2D>().velocity;
			Debug.Log(stuck.GetComponent<Rigidbody2D>().velocity);
		}
		else
		{
			stuck = null;
		}
	}
}
