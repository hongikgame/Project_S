using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SwimState : StateBase
{
    private float _gravityScale;
    private int _animInputXHash;

    public SwimState(PlayerCharacterBase ownerCharacter) : base(ownerCharacter)
    {
        _animationHash = Animator.StringToHash("Swim");
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

        Vector2 normalizedInput = _ownerCharacter.Input.normalized;
        Vector2 velocity = new Vector2(normalizedInput.x * 5.0f, normalizedInput.y * 5.0f);
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
