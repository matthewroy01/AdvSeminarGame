using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoliath : Enemy {

	[Header("The Goliath only moves every other step")]
	public bool isResting;

	[Header("Walls for movement")]
	public LayerMask layerImpass;

	private Vector2 vgUp, vgDown, vgLeft, vgRight;

	[Header("Panic behavior")]
	public ParticleSystem parts;
	public bool enraged = false;
	public AudioSource roar;

	void Start ()
	{
		parts.Stop();
	}

	void Update ()
	{
		Movement();
	}

	public override void Step()
	{
		if (isResting == false && enraged == false)
		{
			// path towards the player
			float lowestHeuristic = Vector2.Distance (getGameManager().player.transform.position, vUp);

			// if the enemy gets stuck in a corner, perform a reset to the heuristic
			if (currentDirection == Dirs.none)
			{
				lowestHeuristic = 100000;
			}

	        // do this as placeholder
	        UpdateVectors();

			// check to see if there's a wall in the spot the enemy is about to move, and if not, calculate the heuristic
			// then set the currentDirection to where the lowest heuristic was calculated
			if (checkMov(Dirs.up))
			{
				if (previousDirection != Dirs.down)
				{
					lowestHeuristic = Vector2.Distance (getGameManager().player.transform.position, vUp);
					currentDirection = Dirs.up;
				}
			}
			if (checkMov(Dirs.down))
			{
				if (lowestHeuristic > Vector2.Distance(getGameManager().player.transform.position, vDown) && previousDirection != Dirs.up)
				{
					lowestHeuristic = Vector2.Distance(getGameManager().player.transform.position, vDown);
					currentDirection = Dirs.down;
				}
			}
			if (checkMov(Dirs.left))
			{
				if (lowestHeuristic > Vector2.Distance(getGameManager().player.transform.position, vLeft) && previousDirection != Dirs.right)
				{
					lowestHeuristic = Vector2.Distance(getGameManager().player.transform.position, vLeft);
					currentDirection = Dirs.left;
				}
			}
			if (checkMov(Dirs.right))
			{
				if (lowestHeuristic > Vector2.Distance(getGameManager().player.transform.position, vRight) && previousDirection != Dirs.left)
				{
					lowestHeuristic = Vector2.Distance(getGameManager().player.transform.position, vRight);
					currentDirection = Dirs.right;
				}
			}

			// now move based on what was decided
			if (currentDirection == Dirs.up && checkMov (Dirs.up))
			{
				transform.position = vUp;
			}
			else if (currentDirection == Dirs.down && checkMov (Dirs.down))
			{
				transform.position = vDown;
			}
			else if (currentDirection == Dirs.left && checkMov (Dirs.left))
			{
				transform.position = vLeft;
			}
			else if (currentDirection == Dirs.right && checkMov (Dirs.right))
			{
				transform.position = vRight;
			}

			// set the previous direction so the enemy can't choose to go back the way it came
			previousDirection = currentDirection;

			// set the direction to none so the enemy doesn't get stuck traveling in its previous direction when no compromise can be reached
			currentDirection = Dirs.none;

			isResting = true;
		}
		else
		{
			isResting = false;
		}
	}

	public override void StepOwn()
	{
		if (enraged == true && getGameManager().paused == false)
		{
			// path towards the player
			float lowestHeuristic = Vector2.Distance (getGameManager().player.transform.position, vUp);

			// if the enemy gets stuck in a corner, perform a reset to the heuristic
			if (currentDirection == Dirs.none)
			{
				lowestHeuristic = 100000;
			}

			// do this as placeholder
			UpdateVectors();

			// check to see if there's a wall in the spot the enemy is about to move, and if not, calculate the heuristic
			// then set the currentDirection to where the lowest heuristic was calculated
			if (checkMov(Dirs.up))
			{
				if (previousDirection != Dirs.down)
				{
					lowestHeuristic = Vector2.Distance (getGameManager().player.transform.position, vUp);
					currentDirection = Dirs.up;
				}
			}
			if (checkMov(Dirs.down))
			{
				if (lowestHeuristic > Vector2.Distance(getGameManager().player.transform.position, vDown) && previousDirection != Dirs.up)
				{
					lowestHeuristic = Vector2.Distance(getGameManager().player.transform.position, vDown);
					currentDirection = Dirs.down;
				}
			}
			if (checkMov(Dirs.left))
			{
				if (lowestHeuristic > Vector2.Distance(getGameManager().player.transform.position, vLeft) && previousDirection != Dirs.right)
				{
					lowestHeuristic = Vector2.Distance(getGameManager().player.transform.position, vLeft);
					currentDirection = Dirs.left;
				}
			}
			if (checkMov(Dirs.right))
			{
				if (lowestHeuristic > Vector2.Distance(getGameManager().player.transform.position, vRight) && previousDirection != Dirs.left)
				{
					lowestHeuristic = Vector2.Distance(getGameManager().player.transform.position, vRight);
					currentDirection = Dirs.right;
				}
			}

			// now move based on what was decided
			if (currentDirection == Dirs.up && checkMov (Dirs.up))
			{
				transform.position = vUp;
			}
			else if (currentDirection == Dirs.down && checkMov (Dirs.down))
			{
				transform.position = vDown;
			}
			else if (currentDirection == Dirs.left && checkMov (Dirs.left))
			{
				transform.position = vLeft;
			}
			else if (currentDirection == Dirs.right && checkMov (Dirs.right))
			{
				transform.position = vRight;
			}

			// set the previous direction so the enemy can't choose to go back the way it came
			previousDirection = currentDirection;

			// set the direction to none so the enemy doesn't get stuck traveling in its previous direction when no compromise can be reached
			currentDirection = Dirs.none;
		}
	}

	private bool checkMov (Dirs dir)
	{
		// if a wall is detected, movement is impossible
		if (dir == Dirs.up)
		{
			if (!Physics2D.OverlapCircle(vgUp, 0.2f, layerImpass))
			{
				return true;
			}
		}
		else if (dir == Dirs.down)
		{
			if (!Physics2D.OverlapCircle(vgDown, 0.2f, layerImpass))
			{
				return true;
			}
		}
		else if (dir == Dirs.left)
		{
			if (!Physics2D.OverlapCircle(vgLeft, 0.2f, layerImpass))
			{
				return true;
			}
		}
		else if (dir == Dirs.right)
		{
			if (!Physics2D.OverlapCircle(vgRight, 0.2f, layerImpass))
			{
				return true;
			}
		}
		return false;
	}

	private void Movement ()
	{
		if (getGameManager().panicMode == true)
		{
			Vector2 tmp = getGameManager().player.transform.position - transform.position;

			if (Mathf.Abs(tmp.x) > Mathf.Abs(tmp.y))
			{
				//Debug.Log("x is greater than y");
			}
			else
			{
				//Debug.Log("y is greater than x");
			}
		}
	}

	public override void UpdateVectors()
    {
		vUp = new Vector2(transform.position.x, transform.position.y + 1);
        vDown = new Vector2(transform.position.x, transform.position.y - 1);
        vLeft = new Vector2(transform.position.x - 1, transform.position.y);
        vRight = new Vector2(transform.position.x + 1, transform.position.y);

        vgUp = new Vector2(transform.position.x, transform.position.y + 1.5f);
        vgDown = new Vector2(transform.position.x, transform.position.y - 1.5f);
        vgLeft = new Vector2(transform.position.x - 1.5f, transform.position.y);
        vgRight = new Vector2(transform.position.x + 1.5f, transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    	if (other.CompareTag("Enemy"))
    	{
    		other.gameObject.transform.position = new Vector2(-1000, -1000);
    		other.gameObject.SetActive(false);
    	}
    }

	public override void Panic(bool state)
	{
		if (state == true)
		{
			parts.Play();
			enraged = false;
			StopAllCoroutines();
		}
		else
		{
			parts.Stop();
			parts.Clear();
			if (getGameManager().panicMode == true)
			{
				enraged = true;
				roar.Play();

				// start pathfinding
				StartCoroutine(onGoliathsOwnTime(panicTime));
			}
		}
	}

	public IEnumerator onGoliathsOwnTime(float time)
	{
		while (enraged)
		{
			StepOwn();
			yield return new WaitForSeconds(time);
		}
	}
}
