using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoppelganger : Enemy {

	[Header("Walls")]
	public LayerMask wallLayer;

	void Update ()
	{
		Movement();
	}

	void Movement ()
	{
		if (getGameManager().DoneWithAnimations() == true)
		{
			if (Input.GetButtonDown("Up") && checkMov(Dirs.down))
			{
				transform.position = new Vector2(transform.position.x, transform.position.y - 1);
			}
			else if (Input.GetButtonDown("Down") && checkMov(Dirs.up))
			{
				transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			}
			else if (Input.GetButtonDown("Left") && checkMov(Dirs.right))
			{
				transform.position = new Vector2(transform.position.x + 1, transform.position.y);
			}
			else if (Input.GetButtonDown("Right") && checkMov(Dirs.left))
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
}
