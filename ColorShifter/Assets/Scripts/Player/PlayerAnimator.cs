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

        // Player animations with touch controls
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
            {
                animator.ResetTrigger("IsLeft");
                animator.SetTrigger("IsRight");
            }
            if (touch.position.x < Screen.width / 2)
            {
                animator.ResetTrigger("IsRight");
                animator.SetTrigger("IsLeft");
            }
        }
    }
}
