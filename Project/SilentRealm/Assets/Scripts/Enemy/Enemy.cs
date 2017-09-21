using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : God {

	[Header("Length of vision in one direction")]
    public float visionDist;

	[Header("Time in between steps while in panic mode")]
    public float panicTime;

	[Header("The direction this enemy is currently facing")]
	public Dirs currentDirection;

	public virtual void Step()
	{
		Debug.Log("ENEMY_STEP - the virtual function was called");
	}

    public virtual void StepOwn()
    {
        Debug.Log("ENEMY_STEP_OWN - the virtual function was called");
    }

    public IEnumerator onYourOwnTime(float time)
    {
        while (panicMode)
        {
            StepOwn();
            yield return new WaitForSeconds(time);
        }
    }

	public virtual void Panic(bool state)
	{
		
	}
}