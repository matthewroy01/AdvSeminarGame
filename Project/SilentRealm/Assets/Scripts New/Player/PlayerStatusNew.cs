using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusNew : MonoBehaviour
{
    // references to other player scripts
    [HideInInspector]
    public PlayerMovementNew refPlayerMovement;
    [HideInInspector]
    public PlayerCollisionNew refPlayerCollision;
    [HideInInspector]
    public PlayerAudioNew refPlayerAudio;
    [HideInInspector]
    public PlayerVisualsNew refPlayerVisuals;

    // references to other components
    [HideInInspector]
    public Rigidbody2D rb;

    // references to external scripts
    [HideInInspector]
    public UtilityGameManager refGameManager;

    public bool isDead = false;
    public bool isWebbed = false;
    public bool won = false;

    private void Start()
    {
        refPlayerMovement = GetComponent<PlayerMovementNew>();
        refPlayerCollision = GetComponent<PlayerCollisionNew>();
        refPlayerAudio = GetComponent<PlayerAudioNew>();
        refPlayerVisuals = GetComponent<PlayerVisualsNew>();

        rb = GetComponent<Rigidbody2D>();

        refGameManager = FindObjectOfType<UtilityGameManager>();
    }

    public void SnapToGrid()
    {
        int tmpx = (int)transform.position.x, tmpy = (int)transform.position.y;

        // snap to the nearest whole number
        // on the X
        if (transform.position.x > tmpx + 0.5)
        {
            transform.position = new Vector2(tmpx + 1, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(tmpx, transform.position.y);
        }

        // on the Y
        if (transform.position.y > tmpy + 0.5)
        {
            transform.position = new Vector2(transform.position.x, tmpy + 1);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, tmpy);
        }
    }
}
