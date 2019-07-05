using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtiliityUnityFunctions : MonoBehaviour {

	public static float Get2dDistance(Vector2 pos1, Vector2 pos2)
    {
        return Vector2.Distance(pos1, pos2);
    }
}
