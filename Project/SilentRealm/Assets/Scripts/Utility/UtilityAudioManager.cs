using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this code taken from Battle Beetles
// written by Matthew Roy

public class UtilityAudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;

    void Awake()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            gameObject.AddComponent<AudioSource>();
        }

        // populating array found here:
        // http://answers.unity3d.com/questions/795797/gather-audiosources-in-an-array.html
        audioSources = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }

    public void PlaySound(AudioClip newAudio, float volume)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying == false)
            {
                audioSources[i].clip = newAudio;
                audioSources[i].pitch = Random.Range(0.95f, 1.05f);
                audioSources[i].volume = volume;
                audioSources[i].Play();
                return;
            }
        }
    }

    public void PlaySound(AudioClip newAudio, float volume, bool doRandomPitch)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying == false)
            {
                audioSources[i].clip = newAudio;
                if (doRandomPitch)
                {
                    audioSources[i].pitch = Random.Range(0.95f, 1.05f);
                }
                else
                {
                    audioSources[i].pitch = 1.0f;
                }
                audioSources[i].volume = volume;
                audioSources[i].Play();
                return;
            }
        }
    }

    public void PlaySound(CustomSound sound, bool doRandomPitch)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying == false)
            {
                audioSources[i].clip = sound.clip;
                if (doRandomPitch)
                {
                    audioSources[i].pitch = Random.Range(0.95f, 1.05f);
                }
                else
                {
                    audioSources[i].pitch = 1.0f;
                }
                audioSources[i].volume = sound.volume;
                audioSources[i].Play();
                return;
            }
        }
    }

    public void PlaySound(AudioClip newAudio, float volume, float pitch)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying == false)
            {
                audioSources[i].clip = newAudio;
                audioSources[i].pitch = pitch;
                audioSources[i].volume = volume;
                audioSources[i].Play();
                return;
            }
        }
    }

    public void PlaySound(AudioClip newAudio, float volume, float pitch_min, float pitch_max)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying == false)
            {
                audioSources[i].clip = newAudio;
                audioSources[i].pitch = Random.Range(pitch_min, pitch_max);
                audioSources[i].volume = volume;
                audioSources[i].Play();
                return;
            }
        }
    }


    public void PlaySound(AudioClip newAudio, float volume, bool doRandomPitch, float delayTime, float pitchLow, float pitchHigh)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying == false)
            {
                audioSources[i].clip = newAudio;
                if (doRandomPitch)
                {
                    audioSources[i].pitch = Random.Range(pitchLow, pitchHigh);
                }
                else
                {
                    audioSources[i].pitch = 1.0f;
                }
                audioSources[i].volume = volume;
                audioSources[i].PlayDelayed(delayTime);
                return;
            }
        }
    }

    public void PlaySound(AudioClip newAudio, float volume, bool doRandomPitch, float delayTime)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying == false)
            {
                audioSources[i].clip = newAudio;
                if (doRandomPitch)
                {
                    audioSources[i].pitch = Random.Range(0.95f, 1.05f);
                }
                else
                {
                    audioSources[i].pitch = 1.0f;
                }
                audioSources[i].volume = volume;
                audioSources[i].PlayDelayed(delayTime);
                return;
            }
        }
    }
}

[System.Serializable]
public struct CustomSound
{
    public AudioClip clip;
    [Range(0.0f, 1.0f)]
    public float volume;
}