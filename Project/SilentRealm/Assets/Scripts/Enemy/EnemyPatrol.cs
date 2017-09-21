using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy {

    // this is serialized so it can appear in the Inspector
    [Header("List of points for patrolling")]
    public PatrolPoint[] points;

    [Header("The end of vision")]
    [SerializeField] private Vector2 endOfVision;

    [Header("Walls for vision")]
    public LayerMask layerWallVision;
    [Header("The player for vision")]
    public LayerMask layerPlayer;
	[Header("Walls for movement")]
	public LayerMask layerImpass;

    void Start () {
		defDir = currentDirection;
		defPos = transform.position;
		UpdateVectors ();
    }

	void Update ()
	{
        Vision();
	}

    private void CheckPoints()
    {
		// if the enemy has reached one of its assigned points, it's time to turn!
        for (int i = 0; i < points.Length; i++)
        {
            if (transform.position == (Vector3)points[i].point)
            {
                currentDirection = points[i].direction;
            }
        }
    }

    public void Vision()
    {
        if (!panicMode)
        {
            if (currentDirection == Dirs.up)
            {
                endOfVision = new Vector2(transform.position.x, transform.position.y + (1 * visionDist));
            }
            else if (currentDirection == Dirs.down)
            {
                endOfVision = new Vector2(transform.position.x, transform.position.y - (1 * visionDist));
            }
            else if (currentDirection == Dirs.left)
            {
                endOfVision = new Vector2(transform.position.x - (1 * visionDist), transform.position.y);
            }
            else if (currentDirection == Dirs.right)
            {
                endOfVision = new Vector2(transform.position.x + (1 * visionDist), transform.position.y);
            }

            // if the player is detected...
            if (Physics2D.Linecast(transform.position, endOfVision, layerPlayer))
            {
                // enter panic mode
                getGameManager().Panic(true);
                Gizmos.color = Color.red;
                Debug.Log(gameObject.name + " found the player!");
            }
            else
            {
                Gizmos.color = Color.white;
            }
        }
    }

    private void OnDrawGizmos()
    {
        // draw a sphere for debugging
        if (!panicMode)
        {
            Gizmos.DrawSphere(new Vector3(endOfVision.x, endOfVision.y, -1), 0.25f);
        }
    }

    public override void Step()
	{
        UpdateVectors();

        // while not in panic mode, move on a set path
		if (currentDirection == Dirs.up)
		{
			transform.position = vUp;
		}
		else if (currentDirection == Dirs.down)
		{
			transform.position = vDown;
		}
		else if (currentDirection == Dirs.left)
		{
			transform.position = vLeft;
		}
		else if (currentDirection == Dirs.right)
		{
			transform.position = vRight;
		}

        // check to see if this enemy has reached a point in its array
        CheckPoints();
	}

    public override void StepOwn()
    {
        // while in panic mode, path towards the player
		float lowestHeuristic = Vector2.Distance (getGameManager().player.transform.position, vUp);

        // do this as placeholder
        UpdateVectors();

		// check to see if there's a wall in the spot the enemy is about to move, and if not, calculate the heuristic
		// then set the currentDirection to where the lowest heuristic was calculated
		if (checkMov(Dirs.up))
		{
			lowestHeuristic = Vector2.Distance (getGameManager().player.transform.position, vUp);
			currentDirection = Dirs.up;
		}
		if (checkMov(Dirs.down))
		{
			if (lowestHeuristic > Vector2.Distance(getGameManager().player.transform.position, vDown))
			{
				lowestHeuristic = Vector2.Distance(getGameManager().player.transform.position, vDown);
				currentDirection = Dirs.down;
			}
		}
		if (checkMov(Dirs.left))
		{
			if (lowestHeuristic > Vector2.Distance(getGameManager().player.transform.position, vLeft))
			{
				lowestHeuristic = Vector2.Distance(getGameManager().player.transform.position, vLeft);
				currentDirection = Dirs.left;
			}
		}
		if (checkMov(Dirs.right))
		{
			if (lowestHeuristic > Vector2.Distance(getGameManager().player.transform.position, vRight))
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
		Debug.ClearDeveloperConsole ();
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

    public override void UpdateVectors()
    {
        vUp = new Vector2(transform.position.x, transform.position.y + 1);
        vDown = new Vector2(transform.position.x, transform.position.y - 1);
        vLeft = new Vector2(transform.position.x - 1, transform.position.y);
        vRight = new Vector2(transform.position.x + 1, transform.position.y);
    }

	public override void Panic(bool state)
	{
		if (state == false)
		{
			// stop the path finding coroutine
			StopAllCoroutines ();

			// only do this if actually in panic mode
			if (panicMode == true)
			{
				// set the position back to default
				transform.position = defPos;

				// set the current direction back to default
				currentDirection = defDir;
			}
		}
		else
		{
			panicMode = state;

			// start pathfinding
			StartCoroutine(onYourOwnTime(panicTime));
		}
		panicMode = state;
	}
}

// struct of points that contains where the enemy should turn and in what direction
[System.Serializable]
public struct PatrolPoint
{
    public Vector2 point;
    public Enemy.Dirs direction;
}