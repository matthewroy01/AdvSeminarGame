using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityDestroyAfterSeconds : MonoBehaviour {

	[Header("This object will be destroyed after this many seconds")]
	public float timeToDestroy;

	[Header("If you want this object to be destroyed on certain layers")]
	public bool destroyOnCollision;
	public LayerMask layerDestroy;

	void Start ()
	{
		Invoke("Dest", timeToDestroy);
	}

	void Update ()
	{
		// using OverlapCircle cause I couldn't get OnCollisionEnter2D to work
		if (Physics2D.OverlapCircle(transform.position, 0.01f, layerDestroy))
		{
			Destroy(gameObject);
		}
	}

	private void Dest()
	{
		Destroy(gameObject);
	}
}
