using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBase
{
    public IdleState(CharacterBase ownerCharacter) : base(ownerCharacter)
    {
    }

    public override void Start()
    {
        base.Start();

        _ownerCharacter.Velocity = new Vector2(0, _ownerCharacter.Velocity.y);
    }
}
