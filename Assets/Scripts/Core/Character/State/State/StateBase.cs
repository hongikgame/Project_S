using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    protected PlayerCharacterBase _ownerCharacter;
    protected int _animationHash;

    public StateBase(PlayerCharacterBase ownerCharacter)
    {
        _ownerCharacter = ownerCharacter;
    }

    public virtual void Start()
    {
        SetAnimation(true);
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Finish()
    {
        SetAnimation(false);
    }

    private void SetAnimation(bool active)
    {
        _ownerCharacter.Animator.SetBool(_animationHash, active);
    }
}
