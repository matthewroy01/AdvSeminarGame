using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFade : MonoBehaviour {

	private SpriteRenderer sprite;
	public float inc;

	void Start ()
	{
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = new Color(
			sprite.color.r,
			sprite.color.g,
			sprite.color.b,
			0);
	}

	void Update ()
	{
		if (sprite.color.a < 255)
		{
			sprite.color = new Color(sprite.color.r,
				sprite.color.g,
				sprite.color.b,
				sprite.color.a + inc);
		}
	}
}
