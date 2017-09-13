using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private GameObject gameManager = null;
	public LayerMask wallLayer;

	void Start () {
		gameManager = GameObject.Find("GameManager");
	}

	void Update () {
		// movement
		Movement();
	}

	private void Movement()
	{
		if (Input.GetButtonDown("Up") && checkMov("up"))
		{
			transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			gameManager.GetComponent<UtilityBroadcast>().togetherNow();
		}
		if (Input.GetButtonDown("Down") && checkMov("down"))
		{
			transform.position = new Vector2(transform.position.x, transform.position.y - 1);
			gameManager.GetComponent<UtilityBroadcast>().togetherNow();
		}
		if (Input.GetButtonDown("Left") && checkMov("left"))
		{
			transform.position = new Vector2(transform.position.x - 1, transform.position.y);
			gameManager.GetComponent<UtilityBroadcast>().togetherNow();
		}
		if (Input.GetButtonDown("Right") && checkMov("right"))
		{
			transform.position = new Vector2(transform.position.x + 1, transform.position.y);
			gameManager.GetComponent<UtilityBroadcast>().togetherNow();
		}
	}

	private bool checkMov (string dir)
	{
		// if a wall is detected, movement is impossible
		if (dir == "up")
		{
			if (!Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 1), 0.2f, wallLayer))
			{
				return true;
			}
		}
		else if (dir == "down")
		{
			if (!Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 1), 0.2f, wallLayer))
			{
				return true;
			}
		}
		else if (dir == "left")
		{
			if (!Physics2D.OverlapCircle(new Vector2(transform.position.x - 1, transform.position.y), 0.2f, wallLayer))
			{
				return true;
			}
		}
		else if (dir == "right")
		{
			if (!Physics2D.OverlapCircle(new Vector2(transform.position.x + 1, transform.position.y), 0.2f, wallLayer))
			{
				return true;
			}
		}
		return false;
	}
}
