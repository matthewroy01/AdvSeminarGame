using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

	private SpriteRenderer sprite;
	public float dec;

	void Start ()
	{
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = new Color(
			sprite.color.r,
			sprite.color.g,
			sprite.color.b,
			1);
	}

	void Update ()
	{
		if (sprite.color.a > 0)
		{
			sprite.color = new Color(sprite.color.r,
				sprite.color.g,
				sprite.color.b,
				sprite.color.a - dec);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
