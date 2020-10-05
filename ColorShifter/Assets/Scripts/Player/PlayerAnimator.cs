using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        // Press Right, Move right, stop left animation
        if (Input.GetKeyDown(KeyCode.RightArrow) && !animator.GetBool("IsRight"))
        {
            animator.SetBool("IsRight", true);
            animator.SetBool("IsLeft", false);
        }

        if(Input.GetKeyUp(KeyCode.RightArrow) && animator.GetBool("IsRight"))
        {
            animator.SetBool("IsRight", false);
        }

        // Press Left, Move left, stop right animation
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !animator.GetBool("IsLeft"))
        {
            animator.SetBool("IsLeft", true);
            animator.SetBool("IsRight", false);
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow) && animator.GetBool("IsLeft"))
        {
            animator.SetBool("IsLeft", false);
        }
    }
}
