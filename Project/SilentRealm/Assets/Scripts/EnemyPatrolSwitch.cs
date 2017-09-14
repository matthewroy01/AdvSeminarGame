using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolSwitch : MonoBehaviour {

	[SerializeField] private string direction;

	public string getDirection()
	{
		return direction;
	}
}
