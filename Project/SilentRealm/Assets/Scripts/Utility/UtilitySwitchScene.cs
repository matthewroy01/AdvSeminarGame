using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UtilitySwitchScene : MonoBehaviour {

	void Start () {
		Invoke("SwitchScene", 1);
	}

	void SwitchScene()
	{
		SceneManager.LoadScene(0);
	}
}
