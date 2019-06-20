using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
    private PlayerStatusNew status;

    [Header("Walls")]
    public LayerMask wallLayer;
    private float checkMovRadius = 0.2f;

    [Header("Movement")]
    public float spdPanic;
    public float gridDelay;
    private bool canMove = true;

    void Start ()
    {
        status = GetComponent<PlayerStatusNew>();
	}
	
	void Update ()
    {
        Movement();
	}

    private void Movement()
    {
        if (!status.isDead && !status.isWebbed)
        {
            // move differently in and out of Panic Mode
            if (!status.refGameManager.panicMode)
            {
                if (canMove)
                {
                    // check for input and then move in the intended direction if possible
                    if (Input.GetButtonDown("Up"))
                    {
                        transform.position = GetNewPosition(Vector2.up);
                    }
                    else if (Input.GetButtonDown("Down"))
                    {
                        transform.position = GetNewPosition(Vector2.down);
                    }
                    else if (Input.GetButtonDown("Left"))
                    {
                        transform.position = GetNewPosition(Vector2.left);
                    }
                    else if (Input.GetButtonDown("Right"))
                    {
                        transform.position = GetNewPosition(Vector2.right);
                    }
                }
            }
            else
            {
                status.rb.velocity = new Vector2(Input.GetAxis("Horizontal") * spdPanic, Input.GetAxis("Vertical") * spdPanic);
            }
        }
    }

    private Vector2 GetNewPosition(Vector2 dir)
    {
        // update the given vector to the intended position
        Vector2 tmp = (Vector2)transform.position + dir;

        // temporarily disable movement
        canMove = false;
        Invoke("ResetMove", gridDelay);

        // move if there's nothing in the way
        if (!Physics2D.OverlapCircle(tmp, checkMovRadius, wallLayer))
        {
            // broadcast to the Game Manager that a movement has taken place
            status.refGameManager.togetherNow();

            return tmp;
        }
        // if a wall is detected, movement is impossible
        else
        {
            return transform.position;
        }
    }

    private void ResetMove()
    {
        canMove = true;
    }
}