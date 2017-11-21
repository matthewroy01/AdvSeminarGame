using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player
{
	public bool movEnabled;

    [Header("Walls")]
	public LayerMask wallLayer;

    [Header("Panic Mode variables and parameters")]
    public float panicSpeed = 0;

	[SerializeField]
	public float animTime;

    // this object's Rigidbody2D
    private Rigidbody2D rb;

    // doppelgangers
    [Header("Doppelgangers")]
    public EnemyDoppelganger[] doppels;

	void Start ()
	{
        // set the Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // initialize the movement vectors
        UpdateVectors();

		webbed = false;
		winState = false;
    }

	void Update ()
	{
		// movement
		Movement();

		//movEnabled = movementEnabled;
	}

	private void Movement()
	{
		if (!getGameManager().paused)
		{
			if(!webbed && !winState)
			{
				if (!getGameManager().panicMode)
		        {
					if (getGameManager().DoneWithAnimations() == true)
					{
			            if (Input.GetButtonDown("Up"))
			            {
							if (checkMov(Dirs.up))
							{
				                transform.position = new Vector2(transform.position.x, transform.position.y + 1);
				                getGameManager().togetherNow();
								SetAnimationState(false);
								Invoke("FinishAnimation", animTime);
							}

							foreach (EnemyDoppelganger doppel in doppels)
							{
									doppel.Movement("up");
							}
			            }
			            else if (Input.GetButtonDown("Down"))
			            {
							if (checkMov(Dirs.down))
							{
				                transform.position = new Vector2(transform.position.x, transform.position.y - 1);
				                getGameManager().togetherNow();
								SetAnimationState(false);
								Invoke("FinishAnimation", animTime);
							}

							foreach (EnemyDoppelganger doppel in doppels)
							{
									doppel.Movement("down");
							}
			            }
			            else if (Input.GetButtonDown("Left"))
			            {
							if (checkMov(Dirs.left))
							{
				                transform.position = new Vector2(transform.position.x - 1, transform.position.y);
				                getGameManager().togetherNow();
								SetAnimationState(false);
								Invoke("FinishAnimation", animTime);
							}

							foreach (EnemyDoppelganger doppel in doppels)
							{
								doppel.Movement("left");
							}
			            }
			            else if (Input.GetButtonDown("Right"))
			            {
							if (checkMov(Dirs.right))
							{
				                transform.position = new Vector2(transform.position.x + 1, transform.position.y);
				                getGameManager().togetherNow();
								SetAnimationState(false);
								Invoke("FinishAnimation", animTime);
							}

							foreach (EnemyDoppelganger doppel in doppels)
							{
								doppel.Movement("right");
							}
			            }
					}
		        }
				else
		        {
					if (!webbed && !winState)
					{
		            	rb.velocity = new Vector2(Input.GetAxis("Horizontal") * panicSpeed, Input.GetAxis("Vertical") * panicSpeed);
					}
		        }
			}
		}
		else
		{
			rb.velocity = Vector2.zero;
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
		//panicMode = state;
	}

	private void FinishAnimation()
	{
		SetAnimationState(true);
	}

	public void DisableMovement()
	{
		// use win state to stop movement as a sort of quick fix
		// this is for when the Striker has the player in its sight
		winState = true;
	}
}
