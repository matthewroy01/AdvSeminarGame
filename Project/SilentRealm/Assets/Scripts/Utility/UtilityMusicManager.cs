using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityMusicManager : MonoBehaviour {

	public float maxVolume;
	public float fadeSpeed;
	public Music panic, calm;

	void Start () 
	{
		// initialze the volumes
		calm.source.volume = maxVolume;
		panic.source.volume	= 0;

		// play both
		calm.source.Play();
		panic.source.Play();

		// don't fade
		calm.fade = 0;
		panic.fade = 0;
	}

	void Update()
	{
		Fade(calm);
		Fade(panic);
	}

	public void Panic(bool state)
	{
		if (state == true)
		{
			panic.fade = 1;
			calm.fade = -1;
		}
		else
		{
			panic.fade = -1;
			calm.fade = 1;
		}
	}

	public void StopAll()
	{
		// make sure the music doesn't fade
		panic.fade = 0;
		calm.fade = 0;

		// stop the music
		panic.source.volume = 0;
		calm.source.volume = 0;
	}

	public void Fade(Music audio)
	{
		if (audio.fade == 0)
		{
			// do nothing
		}

		// fade in
		if (audio.fade == 1)
		{
			// if we're too quiet, get louder and then stop upon reaching the max
			if (audio.source.volume < maxVolume)
			{
				audio.source.volume += fadeSpeed;
			}
			else
			{
				audio.fade = 0;
				audio.source.volume = maxVolume;
			}
		}

		// fade out
		if (audio.fade == -1)
		{
			// if we're too loud, get quieter and then stop upon reaching zero
			if (audio.source.volume > 0)
			{
				audio.source.volume -= fadeSpeed;
			}
			else
			{
				audio.fade = 0;
				audio.source.volume = 0;
			}
		}
	}
}

[System.Serializable]
public struct Music
{
	public AudioSource source;
	public int fade;
};