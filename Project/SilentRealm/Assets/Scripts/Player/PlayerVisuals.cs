using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : Player {

	[Header("Sprites")]
	public GameObject txtWarning;
	public GameObject imgBlack;
	public GameObject imgWhite;

	[Header("Bools")]
	public bool fadeToBlack;
	public bool flashWhite;

	private bool whiteFadeOut = false;

	void Update()
	{
		if (fadeToBlack == true)
		{
			FadeToBlack();
		}

		if (flashWhite == true)
		{
			FlashWhite();
		}
	}

	private void FadeToBlack()
	{
		
		if (imgBlack.GetComponent<SpriteRenderer>().color.a < 255)
		{
			imgBlack.GetComponent<SpriteRenderer>().color = new Color(imgBlack.GetComponent<SpriteRenderer>().color.r,
																	  imgBlack.GetComponent<SpriteRenderer>().color.g,
																	  imgBlack.GetComponent<SpriteRenderer>().color.b,
																	  imgBlack.GetComponent<SpriteRenderer>().color.a + 0.01f);
		}
	}

	private void FlashWhite()
	{
		if (!whiteFadeOut)
		{
			if (imgWhite.GetComponent<SpriteRenderer>().color.a < 250)
			{
			imgBlack.GetComponent<SpriteRenderer>().color = new Color(imgWhite.GetComponent<SpriteRenderer>().color.r,
				imgWhite.GetComponent<SpriteRenderer>().color.g,
				imgWhite.GetComponent<SpriteRenderer>().color.b,
				imgWhite.GetComponent<SpriteRenderer>().color.a + 0.01f);
			}
			else
			{
				whiteFadeOut = true;
			}
		}

		if (whiteFadeOut)
		{
			if (imgWhite.GetComponent<SpriteRenderer>().color.a > 5)
			{
				imgWhite.GetComponent<SpriteRenderer>().color = new Color(imgWhite.GetComponent<SpriteRenderer>().color.r,
					imgWhite.GetComponent<SpriteRenderer>().color.g,
					imgWhite.GetComponent<SpriteRenderer>().color.b,
					imgWhite.GetComponent<SpriteRenderer>().color.a + 0.01f);
			}
			else
			{
				whiteFadeOut = false;
				flashWhite = false;
			}
		}
	}

	private void PanicMessage()
	{
		
	}
}
