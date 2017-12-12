using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityDestroyAfterSeconds : MonoBehaviour {

	[Header("This object will be destroyed after this many seconds")]
	public float timeToDestroy;

	[Header("If you want this object to be destroyed on certain layers")]
	public bool destroyOnCollision;
	public LayerMask layerDestroy;

	private UtilityGameManager ugm;
	private Vector2 pauseVel;
	private Rigidbody2D rb;
	private bool localPause;

	void Start ()
	{
		// we only need to do this if there's a rigidbody in the first place
		if (GetComponent<Rigidbody2D>())
		{
			rb = GetComponent<Rigidbody2D>();
			pauseVel = rb.velocity;
			ugm = GameObject.Find("GameManager").GetComponent<UtilityGameManager>();
		}
		Invoke("Dest", timeToDestroy);
	}

	void Update ()
	{
		// we only need to do this if there's a rigidbody in the first place
		if (GetComponent<Rigidbody2D>())
		{
			if (ugm.paused == true)
			{
				rb.velocity = Vector2.zero;
				CancelInvoke();
				localPause = true;
			}
			else if (localPause == true)
			{
				rb.velocity = pauseVel;
				localPause = false;
				Invoke("Dest", timeToDestroy);
			}
		}

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
