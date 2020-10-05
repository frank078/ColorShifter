using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        // Press Right, Move right, stop left animation
        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.ResetTrigger("IsLeft");
            animator.SetTrigger("IsRight");
        }

        // Press Left, Move left, stop right animation
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.ResetTrigger("IsRight");
            animator.SetTrigger("IsLeft");
        }
    }
}
