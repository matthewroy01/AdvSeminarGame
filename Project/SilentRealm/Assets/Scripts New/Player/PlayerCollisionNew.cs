using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionNew : MonoBehaviour
{
    private PlayerStatusNew status;

    void Start ()
    {
        status = GetComponent<PlayerStatusNew>();
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        // picking up keys
        // layer 12 is "DoppelOnly"
        if (other.CompareTag("Key") && other.gameObject.layer != 12)
        {
            if (status.refGameManager.panicMode == true)
            {
                status.refPlayerAudio.PlayKeyLow();

                // deactivate Panic Mode
                status.refGameManager.Panic(false);
                status.refPlayerVisuals.FadeFromWhite();
                Reset(other);
            }
            else
            {
                status.refPlayerAudio.PlayKeyHigh();
            }

            // increment number of keys collected
            status.refGameManager.keysCollected++;

            // destroy the key
            Destroy(other.gameObject);

            // play particles
            status.refPlayerVisuals.PartsKeyCollect();
        }

        // getting hit by enemies
        if (other.CompareTag("Enemy"))
        {
            Kill();
        }

        // exiting the level and winning
        if (other.gameObject.CompareTag("WinTrigger"))
        {
            status.refGameManager.Panic(false);
            status.refPlayerAudio.PlayCollectAll();
            status.refGameManager.StopAllMusic();

            status.refPlayerVisuals.FadeToBlackWin();

            Reset(other);

            Invoke("Win", 4.0f);
        }
    }

    public void Kill()
    {
        if (!status.isDead)
        {
            status.refGameManager.StopAllMusic();
            status.refPlayerAudio.PlayDeath();

            status.isDead = true;

            status.rb.velocity = Vector2.zero;

            StartCoroutine(Reload());
        }
    }

    void Win()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.0f);

        // fade out
        status.refPlayerVisuals.FadeToBlackDeath();

        yield return new WaitForSeconds(0.3f);

        // reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Reset(Collider2D other)
    {
        // reset the player's position and velocity after exiting Panic Mode
        transform.position = other.transform.position;
        status.rb.velocity = Vector2.zero;
    }
}
