using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : StateBase
{
    private Vector2 _direction;
    public DashState(CharacterBase ownerCharacter) : base(ownerCharacter)
    {
    }

    public override void Start()
    {
        base.Start();

        _direction = _ownerCharacter.Input * 3;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _ownerCharacter.Velocity = _direction * 10;
    }

    public override void Finish()
    {
        base.Finish();


    }
}
