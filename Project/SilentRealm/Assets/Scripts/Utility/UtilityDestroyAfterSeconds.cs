using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityDestroyAfterSeconds : MonoBehaviour {

	[Header("This object will be destroyed after this many seconds")]
	public float timeToDestroy;

	void Start () {
		Invoke("Dest", timeToDestroy);
	}

	private void Dest()
	{
		Destroy(gameObject);
	}
}
