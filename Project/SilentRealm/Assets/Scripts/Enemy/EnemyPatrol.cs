using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy {

    [SerializeField] private bool patrolMode = true;
	public Dirs currentDirection;

    // this is serialized so it can appear in the Inspector
    [Header("List of points for patrolling")]
    public PatrolPoint[] points;

    void Start () {
		
	}

	void Update ()
	{
        
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

	public override void Step()
	{
        // in patrol mode, move according to currentDirection
		if (patrolMode)
		{
			if (currentDirection == Dirs.up)
			{
				transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			}
			else if (currentDirection == Dirs.down)
			{
				transform.position = new Vector2(transform.position.x, transform.position.y - 1);
			}
			else if (currentDirection == Dirs.left)
			{
				transform.position = new Vector2(transform.position.x - 1, transform.position.y);
			}
			else if (currentDirection == Dirs.right)
			{
				transform.position = new Vector2(transform.position.x + 1, transform.position.y);
			}
		}

        // outside of patrol mode, path towards the player
        if (!patrolMode)
        {
            // figure out A* algorithm
        }

        // check to see if this enemy has reached a point in its array
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
}

// struct of points that contains where the enemy should turn and in what direction
[System.Serializable]
public struct PatrolPoint
{
    public Vector2 point;
    public Enemy.Dirs direction;
}