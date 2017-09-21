using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : God
{
    [Header("Walls")]
	public LayerMask wallLayer;

    [Header("Panic Mode variables and parameters")]
    public float panicSpeed = 0;

    // this object's Rigidbody2D
    private Rigidbody2D rb;

	void Start ()
	{
        // set the Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // initialize the movement vectors
        UpdateVectors();
    }

	void Update ()
	{
		// movement
		Movement();
	}

	private void Movement()
	{
        if (!panicMode)
        {
            if (Input.GetButtonDown("Up") && checkMov(Dirs.up))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 1);
                getGameManager().togetherNow();
            }
            if (Input.GetButtonDown("Down") && checkMov(Dirs.down))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                getGameManager().togetherNow();
            }
            if (Input.GetButtonDown("Left") && checkMov(Dirs.left))
            {
                transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                getGameManager().togetherNow();
            }
            if (Input.GetButtonDown("Right") && checkMov(Dirs.right))
            {
                transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                getGameManager().togetherNow();
            }
        }
		else
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * panicSpeed, Input.GetAxis("Vertical") * panicSpeed);
        }
	}

	private bool checkMov (Dirs dir)
	{
        // update the vectors each time movement occurs
        UpdateVectors();

        // if a wall is detected, movement is impossible
        if (dir == Dirs.up)
		{
			if (!Physics2D.OverlapCircle(vUp, 0.2f, wallLayer))
			{
				return true;
			}
		}
		else if (dir == Dirs.down)
		{
			if (!Physics2D.OverlapCircle(vDown, 0.2f, wallLayer))
			{
				return true;
			}
		}
		else if (dir == Dirs.left)
		{
			if (!Physics2D.OverlapCircle(vLeft, 0.2f, wallLayer))
			{
				return true;
			}
		}
		else if (dir == Dirs.right)
		{
			if (!Physics2D.OverlapCircle(vRight, 0.2f, wallLayer))
			{
				return true;
			}
		}
		return false;
	}

	public void Panic(bool state)
	{
		panicMode = state;
	}
}
