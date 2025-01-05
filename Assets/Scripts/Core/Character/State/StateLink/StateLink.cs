using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StateLink
{
    public abstract StateBase OldState { get; }
    public abstract StateBase NewState { get; }

    public abstract bool CheckCondition(CharacterBase character);
    public bool CheckValid()
    {
        return (OldState != null && NewState != null && OldState != NewState);
    }
}