using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : God {

    public float visionDist;
    public float panicTime;

	public virtual void Step()
	{
		Debug.Log("ENEMY_STEP - the virtual function was called");
	}

    public virtual void StepOwn()
    {
        Debug.Log("ENEMY_STEP_OWN - the virtual function was called");
    }

    public virtual void Panic(bool panicing)
    {
        Debug.Log("ENEMY_PANIC - the virtual function was called");
    }

    public IEnumerator onYourOwnTime(float time)
    {
        while (panicMode)
        {
            StepOwn();
            yield return new WaitForSeconds(time);
        }
    }
}