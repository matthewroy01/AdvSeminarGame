using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy {

	[SerializeField] private bool patrolMode = true;
	public string currentDirection;

	void Start () {
		
	}

	void Update ()
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PatrolSwitch")
		{
			currentDirection = other.GetComponent<EnemyPatrolSwitch>().getDirection();
		}
	}

	public  override void Step()
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
