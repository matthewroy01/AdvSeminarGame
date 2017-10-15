using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoppelganger : Enemy {

	[Header("Walls")]
	public LayerMask wallLayer;

	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		defPos = transform.position;
	}

	void Update ()
	{
		if (getGameManager().panicMode == true)
		{
			rb.velocity = new Vector2(
						Input.GetAxis("Horizontal") * getGameManager().player.GetComponent<PlayerMovement>().panicSpeed,
						Input.GetAxis("Vertical") * getGameManager().player.GetComponent<PlayerMovement>().panicSpeed)
						* -1;
		}
	}

	public void Movement (string dir)
	{
		if (getGameManager().panicMode == false)
		{
			if (dir == "up" && checkMov(Dirs.down))
			{
				transform.position = new Vector2(transform.position.x, transform.position.y - 1);
			}
			else if (dir == "down" && checkMov(Dirs.up))
			{
				transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			}
			else if (dir == "left" && checkMov(Dirs.right))
			{
				transform.position = new Vector2(transform.position.x + 1, transform.position.y);
			}
			else if (dir == "right"  && checkMov(Dirs.left))
			{
				transform.position = new Vector2(transform.position.x - 1, transform.position.y);
			}
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

	public override void Panic (bool state)
	{
		if (state == false)
		{
			// only do this if actually in panic mode
			if (getGameManager().panicMode == true)
			{
				// set the position back to default
				transform.position = defPos;

				// set the velocity back to zero
				rb.velocity = Vector2.zero;
			}
		}
		else
		{
			panicMode = state;
		}
		panicMode = state;
	}
}
