using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : StateBase
{
    public WalkState(CharacterBase ownerCharacter) : base(ownerCharacter)
    {
    }

    public override void Start()
    {
        base.Start();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _ownerCharacter.Velocity = new Vector2(_ownerCharacter.Input.x * 5.0f, _ownerCharacter.Velocity.y);
    }
}
