using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PlayerCamera : Player {

	public float followSpeed;

	private PostProcessingBehaviour post;

	void Start ()
	{
		post = GetComponent<PostProcessingBehaviour>();
	}

	void Update ()
	{
		if (getGameManager().panicMode == true)
		{
			Vector3 tmp = getGameManager().player.transform.position;
			transform.position = new Vector3(tmp.x, tmp.y, transform.position.z);

			// enable post processing in panic mode
			post.enabled = true;
		}
		else
		{
			// why lerp when contantly using MoveTowards looks way better?
			Vector3 tmp = Vector3.MoveTowards(transform.position, getGameManager().player.transform.position, followSpeed);
			transform.position = new Vector3(tmp.x, tmp.y, transform.position.z);

			post.enabled = false;
		}
	}
}