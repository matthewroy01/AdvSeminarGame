using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : Enemy {

	[Header("Current chase direction")]
	public bool horizontalChase = true;

	[Header("Walls for movement")]
	public LayerMask layerImpass;

	[Header("Rotation")]
	public bool clockwiseRotation;

	[Header("Webs")]
	public GameObject Web;
	public float webSpeed;
	public float webCooldown;
	public bool canFire = true;

	private Animator anim;

	void Start () {
		defPos = transform.position;
		defDir = currentDirection;

		anim = GetComponent<Animator>();
	}

	void Update () {
		HandleAnimation();
	}

	public override void Step()
	{
		UpdateVectors();

		Fire();

		RotateInPlace();
	}

	public override void StepOwn()
	{
		if (getGameManager().paused == false)
		{
			UpdateVectors ();

			// decide which direction is current
			if (horizontalChase)
			{
				if (transform.position.x > getGameManager().player.transform.position.x)
				{
					currentDirection = Dirs.left;
				}
				if (transform.position.x < getGameManager().player.transform.position.x)
				{
					currentDirection = Dirs.right;
				}
			}
			if (!horizontalChase)
			{
				if (transform.position.y > getGameManager ().player.transform.position.y)
				{
					currentDirection = Dirs.down;
				}
				if (transform.position.y < getGameManager ().player.transform.position.y)
				{
					currentDirection = Dirs.up;
				}
			}

			// now move (if you haven't already reached the target area)
			if (!horizontalChase &&
				(transform.position.y - getGameManager ().player.transform.position.y > 0.5f ||
				transform.position.y - getGameManager ().player.transform.position.y < -0.5f))
			{
				if (currentDirection == Dirs.up && checkMov(Dirs.up))
				{
					transform.position = vUp;
				}
				else if (currentDirection == Dirs.down && checkMov(Dirs.down))
				{
					transform.position = vDown;
				}
				else
				{
					// if a wall is hit, change direction
					// jk that made the spider's behavior inconsistent
					//horizontalChase = !horizontalChase;
				}
			}
			if (horizontalChase &&
				(transform.position.x - getGameManager().player.transform.position.x > 0.5f ||
				transform.position.x - getGameManager().player.transform.position.x < -0.5f))
			{
				if (currentDirection == Dirs.left && checkMov(Dirs.left))
				{
					transform.position = vLeft;
				}
				else if (currentDirection == Dirs.right && checkMov(Dirs.right))
				{
					transform.position = vRight;
				}
				else
				{
					// if a wall is hit, change direction
					// jk that made the spider's behavior inconsistent
					//horizontalChase = !horizontalChase;
				}
			}

			// fire a web if in range
			if (horizontalChase)
			{
				if (transform.position.x - getGameManager().player.transform.position.x < 0.5f &
					transform.position.x - getGameManager().player.transform.position.x > -0.5f)
				{
					FireOwn();
				}
			}
			else
			{
				if (transform.position.y - getGameManager().player.transform.position.y < 0.5f &
					transform.position.y - getGameManager().player.transform.position.y > -0.5f)
				{
					FireOwn();
				}
			}
		}
	}

	public override void Panic(bool state)
	{
		if (state == false)
		{
			// stop the path finding coroutine
			StopAllCoroutines ();

			// only do this if actually in panic mode
			if (getGameManager().panicMode == true)
			{
				// set the position back to default
				transform.position = defPos;

				// set the current direction back to default
				currentDirection = defDir;
			}
		}
		else
		{
			//panicMode = state;

			// start pathfinding
			StartCoroutine(onYourOwnTime(panicTime));
		}
		//panicMode = state;
	}

	private bool checkMov (Dirs dir)
	{
		// if a wall is detected, movement is impossible
		if (dir == Dirs.up)
		{
			if (!Physics2D.OverlapCircle(vUp, 0.2f, layerImpass))
			{
				return true;
			}
		}
		else if (dir == Dirs.down)
		{
			if (!Physics2D.OverlapCircle(vDown, 0.2f, layerImpass))
			{
				return true;
			}
		}
		else if (dir == Dirs.left)
		{
			if (!Physics2D.OverlapCircle(vLeft, 0.2f, layerImpass))
			{
				return true;
			}
		}
		else if (dir == Dirs.right)
		{
			if (!Physics2D.OverlapCircle(vRight, 0.2f, layerImpass))
			{
				return true;
			}
		}
		return false;
	}

	private void FireOwn()
	{
		if (canFire)
		{
			GameObject tmp = Instantiate(Web, transform.position, transform.rotation);
			if (horizontalChase)
			{
				tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, transform.position.y - getGameManager().player.transform.position.y).normalized * webSpeed * -1;
			}
			else
			{
				tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.position.x - getGameManager().player.transform.position.x, 0).normalized * webSpeed * -1;
			}

			canFire = false;
			Invoke("reenableFire", webCooldown);
		}
	}

	private void Fire()
	{
		GameObject tmp = Instantiate(Web, transform.position, transform.rotation);
		if (currentDirection == Dirs.up)
		{
			tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * webSpeed;
		}
		else if (currentDirection == Dirs.down)
		{
			tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * webSpeed;
		}
		else if (currentDirection == Dirs.left)
		{
			tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * webSpeed;
		}
		else if (currentDirection == Dirs.right)
		{
			tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * webSpeed;
		}
	}

	private void reenableFire()
	{
		canFire = true;
	}

	private void RotateInPlace()
	{
		if (clockwiseRotation)
		{
			if (currentDirection == Dirs.up)
			{
				currentDirection = Dirs.right;
			}
			else if (currentDirection == Dirs.right)
			{
				currentDirection = Dirs.down;
			}
			else if (currentDirection == Dirs.down)
			{
				currentDirection = Dirs.left;
			}
			else
			{
				currentDirection = Dirs.up;
			}
		}
		else
		{
			if (currentDirection == Dirs.up)
			{
				currentDirection = Dirs.left;
			}
			else if (currentDirection == Dirs.left)
			{
				currentDirection = Dirs.down;
			}
			else if (currentDirection == Dirs.down)
			{
				currentDirection = Dirs.right;
			}
			else
			{
				currentDirection = Dirs.up;
			}
		}
	}

	void HandleAnimation ()
	{
		anim.SetInteger("dir", (int)currentDirection);
	}
}
