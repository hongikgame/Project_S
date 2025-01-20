using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimState : StateBase
{
    private float _gravityScale;

    public SwimState(CharacterBase ownerCharacter) : base(ownerCharacter)
    {
    }

    public override void Start()
    {
        base.Start();

        _gravityScale = _ownerCharacter.Rigidbody2D.gravityScale;
        _ownerCharacter.Rigidbody2D.gravityScale = 0;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector2 velocity = new Vector2(_ownerCharacter.Input.x * 5.0f, _ownerCharacter.Input.y * 5.0f);
        if(_ownerCharacter.IsInfluenceByFlowWater && _ownerCharacter.FlowWater != null)
        {
            velocity += _ownerCharacter.FlowWater.FlowDirection;
        }

        _ownerCharacter.Velocity = velocity;
    }

    public override void Finish()
    {
        base.Finish();

        _ownerCharacter.Rigidbody2D.gravityScale = _gravityScale;
    }
}
