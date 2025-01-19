using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : StateBase
{
    public float JumpForce = 10f;

    public JumpState(CharacterBase ownerCharacter) : base(ownerCharacter)
    {
    }

    public override void Start()
    {
        base.Start();

        Vector2 velocity = _ownerCharacter.Velocity;
        velocity.y = JumpForce;
        _ownerCharacter.Velocity = velocity;
    }
}
