using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFlash : MonoBehaviour {

	private SpriteRenderer sprite;
	public bool fadeOut = false;
	public float fadeMax;
	public float inc, dec;

	void Start ()
	{
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = new Color(
			sprite.color.r,
			sprite.color.g,
			sprite.color.b,
			0);

		Invoke("dest", 1.0f);
	}

	void Update ()
	{
		if (fadeOut == false)
		{
			sprite.color = new Color(
				sprite.color.r,
				sprite.color.g,
				sprite.color.b,
				sprite.color.a + inc);
		}
		else
		{
			sprite.color = new Color(
				sprite.color.r,
				sprite.color.g,
				sprite.color.b,
				sprite.color.a - dec);
		}
		if (sprite.color.a >= fadeMax)
		{
			fadeOut = true;
		}
	}

	private void dest()
	{
		Destroy(gameObject);
	}
}