﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour {

    // direction vectors
    [HideInInspector]
    public Vector2 vUp, vDown, vLeft, vRight;

	//[Header("Default position")]
	[HideInInspector]
	public Vector2 defPos;

	//[Header("Default direction")]
	[HideInInspector]
	public Dirs defDir;

    // direction facing vector
    [HideInInspector]
    public enum Dirs { none = -1, up = 0, down = 1, left = 2, right = 3 };

    private GameObject gameManager = null;

	[Header("Panic Mode")]
    //public bool panicMode = false;

	[Header("Animation State")]
	private bool doneWithAnimation = true;

	private void Awake()
	{
		FindGameManager();
	}

    public void FindGameManager()
    {
        gameManager = FindObjectOfType<UtilityGameManager>().gameObject;
    }

    private void Update()
    {
        //panicMode = getGameManager().panicMode;
    }

    public virtual void UpdateVectors()
	{
		//Debug.Log("GOD_UPDATE_VECTORS - the virtual function was called");
		// default updates vectors to be the points one unit away from the object
		vUp = new Vector2 (transform.position.x, transform.position.y + 1);
		vDown = new Vector2 (transform.position.x, transform.position.y - 1);
		vLeft = new Vector2 (transform.position.x - 1, transform.position.y);
		vRight = new Vector2 (transform.position.x + 1, transform.position.y);
	}

    public UtilityGameManager getGameManager()
    {
		return gameManager.GetComponent<UtilityGameManager>();
    }

	public bool GetAnimationState() { return doneWithAnimation; }
	public void SetAnimationState(bool newVal) { doneWithAnimation = newVal; }
}