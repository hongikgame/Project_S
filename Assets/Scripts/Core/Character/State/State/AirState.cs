using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : StateBase
{
    public AirState(PlayerCharacterBase ownerCharacter) : base(ownerCharacter)
    {
    }

    public override void Start()
    {
        base.Start();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_ownerCharacter.Input.x == 0)
        {
            _ownerCharacter.Velocity = new Vector2(_ownerCharacter.Velocity.x, _ownerCharacter.Velocity.y);
        }
        else
        {
            _ownerCharacter.Velocity = new Vector2(_ownerCharacter.Input.x * 5.0f, _ownerCharacter.Velocity.y);
        }
    }
}
