using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBug : God {

	[Header("Layers")]
	public LayerMask wallLayer;
	public LayerMask playerLayer;

	void Update()
	{
		checkForPlayer();
		//checkMov(checkForPlayer());
	}

	private Dirs checkForPlayer ()
	{
		// update the vectors each time movement occurs
		UpdateVectors();

		// if the player is detected, it's time to move
		if (Physics2D.OverlapCircle(vUp, 0.2f, playerLayer) && checkMov(Dirs.down))
		{
			Debug.Log("found player up");
			transform.position = new Vector2(transform.position.x, transform.position.y - 1);
			return Dirs.up;
		}
		if (Physics2D.OverlapCircle(vDown, 0.2f, playerLayer) && checkMov(Dirs.up))
		{
			Debug.Log("found player down");
			transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			return Dirs.down;
		}
		if (Physics2D.OverlapCircle(vLeft, 0.2f, playerLayer) && checkMov(Dirs.right))
		{
			Debug.Log("found player left");
			transform.position = new Vector2(transform.position.x + 1, transform.position.y);
			return Dirs.left;
		}
		if (Physics2D.OverlapCircle(vRight, 0.2f, playerLayer) && checkMov(Dirs.left))
		{
			Debug.Log("found player right");
			transform.position = new Vector2(transform.position.x - 1, transform.position.y);
			return Dirs.right;
		}
		return Dirs.none;
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
