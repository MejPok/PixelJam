using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    private Animator m_Animator;

    public static AnimatorControl animatorControl;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        animatorControl = this;
    }

    void Update()
    {
        Run();

        
    }

    bool isIdle;
    bool isRunning;
    bool isJumping;

    public void Jump()
    {
        m_Animator.SetTrigger("Jump");

    }

    public void Run()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) != 0f)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        m_Animator.SetBool("Run", isRunning);
        
        
    }

}
