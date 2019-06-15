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

    private void Start()
    {
        refPlayerMovement = GetComponent<PlayerMovementNew>();
        refPlayerCollision = GetComponent<PlayerCollisionNew>();
        refPlayerAudio = GetComponent<PlayerAudioNew>();
        refPlayerVisuals = GetComponent<PlayerVisualsNew>();

        rb = GetComponent<Rigidbody2D>();

        refGameManager = FindObjectOfType<UtilityGameManager>();
    }
}
