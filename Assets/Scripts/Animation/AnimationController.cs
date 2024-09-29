using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    public readonly UnityEvent DeathEvent = new UnityEvent();

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void ActivateRunAnimation(bool Active)
    {
        _animator.SetBool("Run", Active);
    }
    public void ActivateFastRunAnimation(bool Active)
    {
        _animator.SetBool("Fast Run", Active);
    }
    public void ActivateEatAnimation()
    {
        _animator.SetTrigger("Eat");
    }
    public void ActivateAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
    public void ActivateDeadAnimation()
    {
        _animator.SetTrigger("Death");
    }
    public void DeathEventInvoke()
    {
        DeathEvent?.Invoke();
    }


}
