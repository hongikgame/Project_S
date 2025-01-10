using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_JumpState : StateBase
{
    public G_JumpState(CharacterBase ownerCharacter) : base(ownerCharacter)
    {
    }

    public override void Start()
    {
        base.Start();

        _ownerCharacter.Velocity = new Vector2(_ownerCharacter.Velocity.x, 4f);
    }
}
