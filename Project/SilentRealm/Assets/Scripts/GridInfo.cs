using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo : MonoBehaviour {

	[SerializeField] private bool hasPlayer = false;
	[SerializeField] private bool hasEnemy = false;
	[SerializeField] private bool isWall = false;
	[SerializeField] private bool enemyLight = false;

	public bool getHasPlayer () { return hasPlayer; }
	public bool getHasEnemy () { return hasEnemy; }
	public bool getIsWall () { return isWall; }
	public bool getEnemyLight () { return enemyLight; }

	public void setHasPlayer (bool bl) { hasPlayer = bl; }
	public void setHasEnemy (bool bl) { hasEnemy = bl; }
	public void setIsWall (bool bl) { isWall = bl; }
	public void setEnemyLight (bool bl) { enemyLight = bl; }
}
