using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy {

    [SerializeField] private bool patrolMode = true;
	public Dirs currentDirection;

    // this is serialized so it can appear in the Inspector
    [Header("List of points for patrolling")]
    public PatrolPoint[] points;

    [Header("The end of vision")]
    [SerializeField] private Vector2 endOfVision;

    [Header("Walls for vision")]
    public LayerMask layerWall;
    [Header("The player for vision")]
    public LayerMask layerPlayer;

    void Start () {
        // find the game manager
        FindGameManager();
    }

	void Update ()
	{
        Vision();
	}

    private void CheckPoints()
    {
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
                getGameManager().panic(true);
                Gizmos.color = Color.red;
                Debug.Log(gameObject.name + " found the player!");
                StartCoroutine(onYourOwnTime(panicTime));
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

        // do this as placeholder
        UpdateVectors();

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

        CheckPoints();
    }

    public override void Panic(bool panicing)
    {
        if (panicing)
        {
            patrolMode = false;
        }
        else
        {
            patrolMode = true;
        }
    }

    public override void UpdateVectors()
    {
        vUp = new Vector2(transform.position.x, transform.position.y + 1);
        vDown = new Vector2(transform.position.x, transform.position.y - 1);
        vLeft = new Vector2(transform.position.x - 1, transform.position.y);
        vRight = new Vector2(transform.position.x + 1, transform.position.y);
    }
}

// struct of points that contains where the enemy should turn and in what direction
[System.Serializable]
public struct PatrolPoint
{
    public Vector2 point;
    public Enemy.Dirs direction;
}