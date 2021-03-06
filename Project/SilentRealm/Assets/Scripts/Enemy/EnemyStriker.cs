﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStriker : Enemy {

    // this is serialized so it can appear in the Inspector
    [Header("List of points for patrolling")]
    public PatrolPoint[] points;

    [Header("Points for vision")]
    [SerializeField] private Vector2 endOfVision;
	[SerializeField] private Vector2 midOfVision;

    [Header("Walls for vision")]
    public LayerMask layerWallVision;
	[Header("For vision (the player and things that would obstruct it)")]
    public LayerMask layerVision;
	[Header("Walls for movement")]
	public LayerMask layerImpass;
	public float tmpVisionDist;

	[Header("Arrow to make it more clear what direction we're about to face")]
	public GameObject arrow;
	public GameObject parts;

	[Header("An actual visual for vision cone")]
	public GameObject cone;

	[Header("Our fire strike")]
	public GameObject strike;
	public bool justFired;
	public float fireSpeed;
	private GameObject currentProj;
	public AudioClip beam;

    void Start () {
		defDir = currentDirection;
		defPos = transform.position;
		UpdateVectors ();
    }

	void Update ()
	{
        Vision();

		// flip our sprite when facing left/right
        if (currentDirection == Dirs.left)
        {
        	GetComponent<SpriteRenderer>().flipX = false;
        }
		if (currentDirection == Dirs.right)
        {
        	GetComponent<SpriteRenderer>().flipX = true;
        }
	}

    private void CheckPoints()
    {
		// if the enemy has reached one of its assigned points, it's time to turn!
        for (int i = 0; i < points.Length; i++)
        {
            if (transform.position == (Vector3)points[i].point)
            {
                currentDirection = points[i].direction;
                arrow.SetActive(false);
            }
        }

        UpdateArrow();
    }

    public void Vision()
    {
		if (!getGameManager().panicMode && !justFired)
		{
			// set end of vision point
			// set mid of vision point
			// set the rotation of the cone
            if (currentDirection == Dirs.up)
            {
                endOfVision = new Vector2(transform.position.x, transform.position.y + (1 * visionDist));
				cone.transform.rotation = Quaternion.Euler(90, 0, 0);
            }
            else if (currentDirection == Dirs.down)
            {
                endOfVision = new Vector2(transform.position.x, transform.position.y - (1 * visionDist));
				cone.transform.rotation = Quaternion.Euler(270, 0, 0);
            }
            else if (currentDirection == Dirs.left)
            {
                endOfVision = new Vector2(transform.position.x - (1 * visionDist), transform.position.y);
				cone.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (currentDirection == Dirs.right)
            {
                endOfVision = new Vector2(transform.position.x + (1 * visionDist), transform.position.y);
				cone.transform.rotation = Quaternion.Euler(0, 270, 0);
            }

			// fire the raycast
			RaycastHit2D tmp;
			tmp = Physics2D.Linecast(transform.position, endOfVision, layerVision);
			
            // if the player is detected before any walls...
			if (tmp.collider != null && tmp.collider.gameObject.CompareTag("Player"))
            {
            	// fire fireStrike at the player
            	currentProj = Instantiate(strike, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
            	justFired = true;
				getGameManager().player.GetComponent<PlayerMovement>().DisableMovement();
				getGameManager().FXManager.PlaySound(beam, 0.5f);
            	Invoke("Fire", 0.2f);
            	//Invoke("Reload", 1.0f);
            }
			else if (tmp.collider != null && tmp.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
			{
				endOfVision = tmp.point;
				tmpVisionDist = ((Vector2)transform.position - tmp.point).magnitude;
			}
			else
			{
				tmpVisionDist = visionDist;
			}

			SetMidVision();

			// set the cone's position to the mid position
			cone.transform.position = midOfVision;
			// set the scale of the cone according to the visionDist
			cone.transform.localScale = new Vector3(cone.transform.localScale.x, cone.transform.localScale.y, tmpVisionDist + 1.0f);
        }
    }

	private void Fire() { currentProj.GetComponent<Rigidbody2D>().velocity = (getGameManager().player.transform.position - transform.position).normalized * fireSpeed; }
    //private void Reload() { justFired = false; }

	private void SetMidVision()
	{
		midOfVision = new Vector3((transform.position.x + endOfVision.x) * 0.5f, (transform.position.y + endOfVision.y) * 0.5f, 0);
	}

    private void UpdateArrow()
    {
    	UpdateVectors();
    	if (currentDirection == Dirs.up)
    	{
			for (int i = 0; i < points.Length; i++)
        	{
	            if (points[i].point == vUp && points[i].direction == Dirs.up)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
					arrow.transform.position = vUp;
	            }
				if (points[i].point == vUp && points[i].direction == Dirs.down)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
					arrow.transform.position = vUp;
	            }
				if (points[i].point == vUp && points[i].direction == Dirs.right)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
					arrow.transform.position = vUp;
	            }
				if (points[i].point == vUp && points[i].direction == Dirs.left)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 270);
					arrow.transform.position = vUp;
	            }
        	}
    	}

		if (currentDirection == Dirs.down)
    	{
			for (int i = 0; i < points.Length; i++)
        	{
	            if (points[i].point == vDown && points[i].direction == Dirs.up)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
					arrow.transform.position = vDown;
	            }
				if (points[i].point == vDown && points[i].direction == Dirs.down)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
					arrow.transform.position = vDown;
	            }
				if (points[i].point == vDown && points[i].direction == Dirs.right)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
					arrow.transform.position = vDown;
	            }
				if (points[i].point == vDown && points[i].direction == Dirs.left)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 270);
					arrow.transform.position = vDown;
	            }
        	}
    	}

		if (currentDirection == Dirs.left)
    	{
			for (int i = 0; i < points.Length; i++)
        	{
	            if (points[i].point == vLeft && points[i].direction == Dirs.up)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
					arrow.transform.position = vLeft;
	            }
				if (points[i].point == vLeft && points[i].direction == Dirs.down)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
					arrow.transform.position = vLeft;
	            }
				if (points[i].point == vLeft && points[i].direction == Dirs.right)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
					arrow.transform.position = vLeft;
	            }
				if (points[i].point == vLeft && points[i].direction == Dirs.left)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 270);
					arrow.transform.position = vLeft;
	            }
        	}
    	}

		if (currentDirection == Dirs.right)
    	{
			for (int i = 0; i < points.Length; i++)
        	{
	            if (points[i].point == vRight && points[i].direction == Dirs.up)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
					arrow.transform.position = vRight;
	            }
				if (points[i].point == vRight && points[i].direction == Dirs.down)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
					arrow.transform.position = vRight;
	            }
				if (points[i].point == vRight && points[i].direction == Dirs.right)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
					arrow.transform.position = vRight;
	            }
				if (points[i].point == vRight && points[i].direction == Dirs.left)
	            {
	            	arrow.SetActive(true);
					arrow.transform.rotation = Quaternion.Euler(0, 0, 270);
					arrow.transform.position = vRight;
	            }
        	}
    	}
    }

    /*private void OnDrawGizmos()
    {
        // draw a sphere for debugging
		if (!getGameManager().panicMode)
        {
            //Gizmos.DrawSphere(new Vector3(endOfVision.x, endOfVision.y, -1), 0.25f);
			Gizmos.DrawLine(transform.position, new Vector3(endOfVision.x, endOfVision.y, -1));
        }
    }*/

    public override void Step()
	{
        UpdateVectors();

        if (points.Length != 0)
        {
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
	}

    public override void StepOwn()
    {
        // ???
        // results
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

			// enable the vision cone
			cone.SetActive(true);

			// only do this if actually in panic mode
			if (getGameManager().panicMode == true)
			{
				// set the position back to default
				transform.position = defPos;

				// set the current direction back to default
				currentDirection = defDir;

				// show particle effects
				Instantiate(parts, transform.position, transform.rotation);
			}
		}
		else
		{
			// disable the vision cone
			cone.SetActive(false);

			// show particle effects
			Instantiate(parts, transform.position, transform.rotation);

			transform.position = new Vector2(1000, 1000);

			// start pathfinding
			StartCoroutine(onYourOwnTime(panicTime));
			arrow.SetActive(false);
		}
	}
}