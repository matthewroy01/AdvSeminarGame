using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsNew : MonoBehaviour
{
    public GameObject deathFade;
    public GameObject winFade;
    public GameObject whiteFlash;

    public ParticleSystem keyCollect;

    public void FadeToBlackDeath()
    {
        Instantiate(deathFade, new Vector3(transform.position.x, transform.position.y, -5), transform.rotation);
    }

    public void FadeToBlackWin()
    {
        Instantiate(winFade, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation);
    }

    public void FadeFromWhite()
    {
        Instantiate(whiteFlash, new Vector3(transform.position.x, transform.position.y, -2), transform.rotation);
    }

    public void PartsKeyCollect()
    {
        Instantiate(keyCollect, transform.position, transform.rotation);
    }
}