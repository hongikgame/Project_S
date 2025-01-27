using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimState : StateBase
{
    public SwimState(CharacterBase ownerCharacter) : base(ownerCharacter)
    {
    }

    public override void Start()
    {
        base.Start();

        //_ownerCharacter.Animator?.SetBool("walk", true);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _ownerCharacter.Velocity = new Vector2(_ownerCharacter.Input.x * 5.0f, _ownerCharacter.Input.y * 5.0f);
    }
}
