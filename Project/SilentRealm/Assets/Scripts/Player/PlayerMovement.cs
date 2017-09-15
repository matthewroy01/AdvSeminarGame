using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : God {

    [SerializeField] private GameObject gameManager = null;
	public LayerMask wallLayer;

	void Start () {
        // find the GameManager
		gameManager = GameObject.Find("GameManager");
        if(gameManager == null)
        {
            Debug.Log("PLAYER_START - game manager not found");
        }
        // initialize the movement vectors
        UpdateVectors();
    }

	void Update () {
		// movement
		Movement();
	}

	private void Movement()
	{
		if (Input.GetButtonDown("Up") && checkMov(Dirs.up))
		{
            transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			gameManager.GetComponent<UtilityBroadcast>().togetherNow();
		}
		if (Input.GetButtonDown("Down") && checkMov(Dirs.down))
		{
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);
			gameManager.GetComponent<UtilityBroadcast>().togetherNow();
		}
		if (Input.GetButtonDown("Left") && checkMov(Dirs.left))
		{
            transform.position = new Vector2(transform.position.x - 1, transform.position.y);
			gameManager.GetComponent<UtilityBroadcast>().togetherNow();
		}
		if (Input.GetButtonDown("Right") && checkMov(Dirs.right))
		{
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
			gameManager.GetComponent<UtilityBroadcast>().togetherNow();
		}
	}

	private bool checkMov (Dirs dir)
	{
        // update the vectors each time movement occurs
        UpdateVectors();

        // if a wall is detected, movement is impossible
        if (dir == Dirs.up)
		{
			if (!Physics2D.OverlapCircle(vUp, 0.2f, wallLayer))
			{
				return true;
			}
		}
		else if (dir == Dirs.down)
		{
			if (!Physics2D.OverlapCircle(vDown, 0.2f, wallLayer))
			{
				return true;
			}
		}
		else if (dir == Dirs.left)
		{
			if (!Physics2D.OverlapCircle(vLeft, 0.2f, wallLayer))
			{
				return true;
			}
		}
		else if (dir == Dirs.right)
		{
			if (!Physics2D.OverlapCircle(vRight, 0.2f, wallLayer))
			{
				return true;
			}
		}
		return false;
	}
}
