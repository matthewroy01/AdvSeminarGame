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
            status.won = true;

            status.refGameManager.Panic(false);
            status.refPlayerAudio.PlayCollectAll();
            status.refGameManager.StopAllMusic();

            status.refPlayerVisuals.FadeToBlackWin();

            Reset(other);

            Invoke("Win", 4.0f);
        }

        // webs
        if (other.gameObject.CompareTag("Web"))
        {
            if (!status.isWebbed && !status.won && !status.isDead)
            {
                status.isWebbed = true;
                status.rb.velocity = other.GetComponent<Rigidbody2D>().velocity;

                status.refPlayerAudio.PlayWebHit();
            }

            // ignore collision with voids
            Physics2D.IgnoreLayerCollision(gameObject.layer, 11, true);
            Destroy(other.gameObject);
        }

        // entering a spotlight's trigger
        if (other.gameObject.CompareTag("Spotlight") && !status.refGameManager.panicMode)
        {
            status.refGameManager.Panic(true);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 8 && status.isWebbed)
        {
            // otherwise, just reenable movement
            if (status.refGameManager.panicMode)
            {
                status.isWebbed = false;
            }
            // hitting a wall while webbed and not in panic mode should snap the player back to the grid
            else
            {
                status.isWebbed = false;
                status.SnapToGrid();
            }

            // stop ignoring collision with voids
            Physics2D.IgnoreLayerCollision(gameObject.layer, 11, false);
            status.refPlayerAudio.PlayWebFree();
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
