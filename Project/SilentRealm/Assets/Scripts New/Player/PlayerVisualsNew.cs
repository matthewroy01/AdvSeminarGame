using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsNew : MonoBehaviour
{
    public GameObject deathFade;

    public void FadeToBlackDeath()
    {
        Instantiate(deathFade, new Vector3(transform.position.x, transform.position.y, -5), transform.rotation);
    }
}
