using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour {

	private MenuNavigation menu;
	public float range;
	public float movSpeed;
	public float distance;
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
			Vector3 myPos = new Vector3(tmp.x, tmp.y, transform.position.z);
			distance = Mathf.Abs((transform.position.y - myPos.y)) - range;
			if (distance < 0)
			{
				distance = 0;
			}
			if (tmp.y > transform.position.y + range || tmp.y < transform.position.y - range)
			{
				transform.position = Vector3.MoveTowards(transform.position, myPos, movSpeed * distance);
			}
		}
	}
}
