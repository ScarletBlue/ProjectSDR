using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniamtionTest : MonoBehaviour {

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetTrigger("Idle");
        }

        else if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetTrigger("Move");
            animator.Play("MelonPixie_Move");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetTrigger("Dash");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            animator.SetTrigger("RisingJump");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            animator.SetTrigger("BasicAttack");
        }

        else if (Input.GetKey(KeyCode.Alpha6))
        {
            animator.SetBool("Skill1Bool",true);
        }

        else if (!Input.GetKey(KeyCode.Alpha6))
        {
            animator.SetBool("Skill1Bool", false);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            animator.SetTrigger("Utimate");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            animator.SetTrigger("BeAttacked");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            animator.SetTrigger("Die");
        }
    }
}
