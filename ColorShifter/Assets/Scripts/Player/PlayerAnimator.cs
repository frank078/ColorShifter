using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !animator.GetBool("IsRight"))
        {
            animator.SetBool("IsRight", true);
        }

        if(Input.GetKeyUp(KeyCode.RightArrow) && animator.GetBool("IsRight"))
        {
            animator.SetBool("IsRight", false);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) && !animator.GetBool("IsLeft") && !animator.GetBool("IsRight"))
        {
            animator.SetBool("IsLeft", true);
            animator.SetBool("IsRight", true);
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow) && animator.GetBool("IsLeft") && animator.GetBool("IsRight"))
        {
            animator.SetBool("IsLeft", false);
            animator.SetBool("IsRight", false);
        }
    }
}
