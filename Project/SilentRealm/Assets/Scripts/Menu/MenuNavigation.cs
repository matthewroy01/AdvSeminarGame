﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour {

	[Header("Is this menu active")]
	public bool isActive;

	[Header("Buttons")]
	public int currentSelection = 0;
	private int maxSelection;

	public GameObject[] buttons;

	public AudioSource cursor;

	void Start ()
	{
		maxSelection = buttons.Length - 1;
	}

	void Update ()
	{
		checkInput();
		setColor();
	}

	void checkInput ()
	{
		if (isActive)
		{
			if (Input.GetButtonDown("Up"))
			{
				if (currentSelection - 1 < 0)
				{
					currentSelection = maxSelection;
				}
				else
				{
					currentSelection--;
				}

				cursor.Play();
			}

			if (Input.GetButtonDown("Down"))
			{
				if (currentSelection + 1 > maxSelection)
				{
					currentSelection = 0;
				}
				else
				{
					currentSelection++;
				}

				cursor.Play();
			}

			if (Input.GetButtonDown("Escape"))
			{
				GameObject.Find("Menu Control").GetComponent<MenuControl>().setNewAsActive("Main Menu");
				GameObject.Find("Menu Control").GetComponent<MenuControl>().MoveCamera();
			}
		}
	}

	void setColor()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			if (i == currentSelection)
			{
				buttons[i].GetComponent<SpriteRenderer>().color = Color.green;
			}
			else
			{
				buttons[i].GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
	}
}
