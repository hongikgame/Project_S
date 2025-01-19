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

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(_ownerCharacter.Input.x == 0)
        {
            _ownerCharacter.Velocity = new Vector2(_ownerCharacter.Velocity.x, _ownerCharacter.Velocity.y);
        }
        else
        {
            _ownerCharacter.Velocity = new Vector2(_ownerCharacter.Input.x * 5.0f, _ownerCharacter.Velocity.y);
        }
        
    }
}
