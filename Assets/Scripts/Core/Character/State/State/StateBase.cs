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
        
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Finish()
    {
        
    }

    public virtual void UpdateAnimation()
    {
        
    }

    public virtual void UpdateRotation()
    {
    }

    protected void PlayAnimation(int hash)
    {
        _ownerCharacter.Animator.Play(hash);
    }
}
