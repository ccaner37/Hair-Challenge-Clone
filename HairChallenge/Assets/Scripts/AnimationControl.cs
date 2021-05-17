using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isRunning)
        {
            animator.SetBool("isRunningAnim", true);
        }
        else
        {
            animator.SetBool("isRunningAnim", false);
        }
    }
}
