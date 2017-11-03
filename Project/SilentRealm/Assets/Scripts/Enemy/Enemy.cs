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
	public Dirs previousDirection = Dirs.none;

	[Header("Time to wait for player to get ahead upon entering panic mode")]
	public float leeway;

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
    	yield return new WaitForSeconds(leeway);

		while (getGameManager().panicMode)
        {
            StepOwn();
            yield return new WaitForSeconds(time);
        }
    }

	public virtual void Panic(bool state)
	{
		Debug.Log("ENEMY_PANIC - the virtual function was called");
		getGameManager().panicMode = state;
	}
}

// struct of points that contains where the enemy should turn and in what direction
[System.Serializable]
public struct PatrolPoint
{
    public Vector2 point;
    public Enemy.Dirs direction;
}