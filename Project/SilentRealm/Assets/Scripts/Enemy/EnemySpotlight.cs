using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpotlight : Enemy
{
	[Header("Movement variables")]
	public float movSpeed;
	public float waitTime;
	public float chaseSpeed;

	[Header("List of points for patrolling")]
    public PatrolPoint[] points;
	public float checkAccuracy;

    private Rigidbody2D rb;
	private bool canMove = false;

	private int count = 0, maxCount;

    void Start ()
    {
    	rb = GetComponent<Rigidbody2D>();
		maxCount = points.Length;
		Invoke("ReenableMovement", waitTime);

		defDir = currentDirection;
		defPos = transform.position;
    }

	void Update ()
	{
		Movement();
	}

	void Movement()
	{
		if (!getGameManager().panicMode)
		{
			// move if we're not paused
			if (getGameManager().paused == false)
			{
				// move if we can move
				if (canMove == true)
				{
					rb.velocity = (points[count].point - (Vector2)transform.position).normalized * movSpeed;
				}
				// else don't move (because we're taking a break)
				else
				{
					rb.velocity = Vector2.zero;
				}

				// check to see if we've reached our next point
				if (CheckPoint())
				{
					if (count == maxCount - 1)
					{
						count = 0;
					}
					else
					{
						count++;
					}
				}
			}
			// else don't move (because the game is paused)
			else
			{
				rb.velocity = Vector2.zero;
			}
		}
		else
		{
			if (!(transform.position.x - getGameManager().player.transform.position.x >= 0.25f * -1 && transform.position.x - getGameManager().player.transform.position.x <= 0.25f &&
				transform.position.y - getGameManager().player.transform.position.y >= 0.25f * -1 && transform.position.y - getGameManager().player.transform.position.y <= 0.25f) && getGameManager().paused == false)
			{
				rb.velocity = ((Vector2)getGameManager().player.transform.position - (Vector2)transform.position).normalized * chaseSpeed;
			}
			else
			{
				rb.velocity = Vector2.zero;
			}
		}
	}

	bool CheckPoint()
	{
		if (currentDirection == Dirs.right || currentDirection == Dirs.left)
		{
			if (transform.position.x - points[count].point.x >= checkAccuracy * -1 && transform.position.x - points[count].point.x <= checkAccuracy)
			{
				// reset the position
				transform.position = new Vector3(points[count].point.x, points[count].point.y, transform.position.z);
				// change the current direction for next time
				currentDirection = points[count].direction;
				// rest at each point
				canMove = false;
				Invoke("ReenableMovement", waitTime);
				return true;
			}
		}
		else if (currentDirection == Dirs.up || currentDirection == Dirs.down)
		{
			if (transform.position.y - points[count].point.y >= checkAccuracy * -1 && transform.position.y - points[count].point.y <= checkAccuracy)
			{
				// reset the position
				transform.position = new Vector3(points[count].point.x, points[count].point.y, transform.position.z);
				// change the current direction for next time
				currentDirection = points[count].direction;
				// rest at each point
				canMove = false;
				Invoke("ReenableMovement", waitTime);
				return true;
			}
		}
		return false;
	}

	void ReenableMovement()
	{
		canMove = true;
	}

	public override void Panic(bool state)
	{
		if (state == false)
		{
			// only do this if actually in panic mode
			if (getGameManager().panicMode == true)
			{
				// set the position back to default
				transform.position = new Vector3(defPos.x, defPos.y, transform.position.z);

				// set the current direction back to default
				currentDirection = defDir;

				// reset the velocity
				rb.velocity = Vector2.zero;

				// reset the count
				count = 0;

				// rest for a bit
				canMove = false;
				Invoke("ReenableMovement", waitTime);
			}
		}
		else
		{
			//panicMode = state;
		}
		//panicMode = state;
	}
}
