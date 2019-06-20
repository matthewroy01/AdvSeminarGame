using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityBreakFromParentOnStart : MonoBehaviour
{
	void Start ()
    {
        transform.SetParent(null);
	}
}
