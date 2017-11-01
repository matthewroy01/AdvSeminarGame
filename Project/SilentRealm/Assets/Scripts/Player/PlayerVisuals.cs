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

	void Update()
	{
		if (fadeToBlack == true)
		{
			FadeToBlack();
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

	}

	private void PanicMessage()
	{
		
	}
}
