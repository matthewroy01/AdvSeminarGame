using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : God
{
    void Start()
    {
        // find the game manager
        FindGameManager();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Key")
        {
            // increase the current number of keys
            getGameManager().keysCollected++;
            // reset the player's velocity and position from panic mode
            PlayerReset(other);
            // stop panic mode
            getGameManager().panic(false);
            // destroy the key
            Destroy(other.gameObject);
        }
    }

    private void PlayerReset(Collider2D other)
    {
        transform.position = other.transform.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
