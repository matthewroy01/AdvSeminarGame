using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code originally from a summer project by Matthew Roy
// text.split: http://answers.unity3d.com/questions/64107/reading-line-wise.html

public class UtilityLevelLoader : MonoBehaviour {

	[SerializeField] public TextAsset asset;
	[SerializeField] public string [] text;

	[SerializeField] private GameObject gridObject = null;

	void Start () {
		Debug.Log(asset.text);

		// divide the text using line breaks and put each string into an array
		text = asset.text.Split("\n"[0]);

		for (int i = 0; i < text.Length; i++)
		{
			for (int j = 0; j < text[i].Length; j++)
			{
				// this grabs the current string (text[i]) and grabs the character at the specified index ([j])
				//Debug.Log("Detecting: " + text[i][j]);

				Debug.Log("Instantiating using " + text[i][j]);

				switch (text[i][j])
				{
					// case '0' is same as default, spawn nothing
					case '1' :
					{
						// normal ground
						GameObject tmp = Instantiate(gridObject, new Vector2(i, j * -1), transform.rotation);
						break;
					}
					case '2' :
					{
						// a wall
						GameObject tmp = Instantiate(gridObject, new Vector2(i, j * -1), transform.rotation);
						tmp.GetComponent<GridInfo>().setIsWall(true);
						break;
					}
					default :
					{
						// do nothing (we don't want anything spawning from a end of line marker)
						break;
					}
				}
			}
		}
	}

	void Update () {

	}
}