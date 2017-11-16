using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour {

	private MenuNavigation menu;
	public float range;
	public Vector2 tmp;

	void Start ()
	{
		menu = GameObject.Find("Level Select").GetComponent<MenuNavigation>();
	}

	void Update ()
	{
		if (menu.isActive)
		{
			tmp = menu.buttons[menu.currentSelection].transform.position;
			if (tmp.y > transform.position.y + range || tmp.y < transform.position.y - range)
			{
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(tmp.x, tmp.y, transform.position.z), 1.0f);
			}
		}
	}
}
