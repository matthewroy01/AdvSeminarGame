using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioNew : MonoBehaviour
{
    [Header("Sounds to be played by the player")]
    public CustomSound collectAll;
    public CustomSound keyHigh;
    public CustomSound keyLow;
    public CustomSound death;
    public CustomSound webHit;
    public CustomSound webFree;

    private UtilityAudioManager refAudioManager;

    private void Start()
    {
        refAudioManager = FindObjectOfType<UtilityAudioManager>();
    }

    public void PlayCollectAll()
    {
        refAudioManager.PlaySound(collectAll, false);
    }

    public void PlayKeyHigh()
    {
        refAudioManager.PlaySound(keyHigh, false);
    }

    public void PlayKeyLow()
    {
        refAudioManager.PlaySound(keyLow, false);
    }

    public void PlayDeath()
    {
        refAudioManager.PlaySound(death, false);
    }

    public void PlayWebHit()
    {
        refAudioManager.PlaySound(webHit, false);
    }

    public void PlayWebFree()
    {
        refAudioManager.PlaySound(webFree, false);
    }
}