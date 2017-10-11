using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpotlight : Enemy
{
	[Header("Movement variables")]
	public float movSpeed;
	public float waitTime;

	[Header("List of points for patrolling")]
    public PatrolPoint[] points;

    private Rigidbody2D rb;

    void Start ()
    {
    	rb = GetComponent<Rigidbody2D>();
    }

	void Update ()
	{
		
	}
}
