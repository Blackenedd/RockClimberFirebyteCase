using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator mAnimator;

    public void Construct()
    {
        mAnimator = GetComponent<Animator>();
    }
    public void Idle()
    {
        mAnimator.SetTrigger("Idle");
    }
    public void Dance()
    {
        mAnimator.SetTrigger("Dance");
    }
    public void Flip()
    {
        mAnimator.SetTrigger("Flip");
    }
    public void Disable()
    {
        mAnimator.enabled = false;
    }
    public void Enable()
    {
        mAnimator.enabled = true;
    }
}
