using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy {

	[SerializeField] private bool patrolMode = true;
	public string currentDirection;

    // this is serialized so it can appear in the Inspector!
    [Header("List of points for patrolling")]
    public PatrolPoint[] points;

void Start () {
		
	}

	void Update ()
	{
        CheckPoints();
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
		Debug.Log("ENEMYPATROL - the overriden function was called");
		// switch direction each time as a test
		if (patrolMode)
		{
			if (currentDirection == "up")
			{
				transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			}
			else if (currentDirection == "down")
			{
				transform.position = new Vector2(transform.position.x, transform.position.y - 1);
			}
			else if (currentDirection == "left")
			{
				transform.position = new Vector2(transform.position.x - 1, transform.position.y);
			}
			else if (currentDirection == "right")
			{
				transform.position = new Vector2(transform.position.x + 1, transform.position.y);
			}
		}
	}
}

[System.Serializable]
public struct PatrolPoint
{
    public Vector2 point;
    public string direction;
}