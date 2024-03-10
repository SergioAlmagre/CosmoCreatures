using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animation))]
public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        _animator.Play("Idle");
    }

    public void PlayRun()
    {
        _animator.Play("Run");
    }
}
