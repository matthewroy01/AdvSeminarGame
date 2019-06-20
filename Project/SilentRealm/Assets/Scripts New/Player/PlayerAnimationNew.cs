using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationNew : MonoBehaviour
{
    private PlayerStatusNew status;
    private Animator refAnimator;

	void Start ()
    {
        status = GetComponent<PlayerStatusNew>();

        refAnimator = GetComponent<Animator>();
	}
	
	void Update ()
    {
        SetAnimations();
	}

    private void SetAnimations()
    {
        refAnimator.SetBool("webbed", status.isWebbed);
        refAnimator.SetBool("dead", status.isDead);
    }
}
