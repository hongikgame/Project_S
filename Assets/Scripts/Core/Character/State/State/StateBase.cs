using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    protected CharacterBase _ownerCharacter;

    public StateBase(CharacterBase ownerCharacter)
    {
        _ownerCharacter = ownerCharacter;
    }

    public virtual void Start()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Finish()
    {

    }

}
