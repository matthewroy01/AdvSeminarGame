using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : Player {

	private float count = 0, interval = 4.5f;

	void Update ()
	{
		// outside panic mode, have the camera lerp to the player's position
		if (getGameManager().panicMode == false)
		{
			if ((Vector2)transform.position != (Vector2)getGameManager ().player.transform.position)
			{
				Vector2 tmp = Vector2.Lerp (transform.position, getGameManager ().player.transform.position, count);
				transform.position = new Vector3 (tmp.x, tmp.y, transform.position.z);
				count += interval * Time.deltaTime;
			}
			else
			{
				count = 0;
			}
		}
		// otherwise, just set the camera's position to the player's
		else
		{
			transform.position = new Vector3(getGameManager().player.transform.position.x, getGameManager().player.transform.position.y, transform.position.z);
		}
	}
}