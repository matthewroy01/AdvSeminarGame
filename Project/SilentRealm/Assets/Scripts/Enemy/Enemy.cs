using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // the direction the enemy is facing
    public enum Dirs { up = 0, down = 1, left = 2, right = 3 };

	public virtual void Step()
	{
		Debug.Log("ENEMYSTEP - the virtual function was called");
	}

    public virtual void Panic(bool panicing)
    {
        Debug.Log("ENEMYPANIC - the virtual function was called");
    }
}