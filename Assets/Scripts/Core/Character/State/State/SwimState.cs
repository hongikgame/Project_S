using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SwimState : StateBase
{
    private float _gravityScale;
    private int _animInputXHash;

    protected int _animationHash_SwimUp;
    protected int _animationHash_SwimUpSide;
    protected int _animationHash_SwimSide;
    protected int _animationHash_SwimDownSide;
    protected int _animationHash_SwimDown;

    public SwimState(PlayerCharacterBase ownerCharacter) : base(ownerCharacter)
    {
        _animationHash_SwimUp = Animator.StringToHash("Swim_Up");
        _animationHash_SwimUpSide = Animator.StringToHash("Swim_UpSide");
        _animationHash_SwimSide = Animator.StringToHash("Swim_Side");
        _animationHash_SwimDownSide = Animator.StringToHash("Swim_DownSide");
        _animationHash_SwimDown = Animator.StringToHash("Swim_Down");
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

    public override void Update()
    {
        base.Update();

    }

    public override void Finish()
    {
        base.Finish();

        _ownerCharacter.Rigidbody2D.gravityScale = _gravityScale;
    }

    public override void UpdateAnimation()
    {
        if(_ownerCharacter.Input.y > 0)
        {
            if(_ownerCharacter.Input.x == 0) PlayAnimation(_animationHash_SwimUp);
            else PlayAnimation(_animationHash_SwimUpSide);
        }
        else if(_ownerCharacter.Input.y == 0)
        {
            PlayAnimation(_animationHash_SwimSide);
        }
        else
        {
            if (_ownerCharacter.Input.x == 0) PlayAnimation(_animationHash_SwimDown);
            else PlayAnimation(_animationHash_SwimDownSide);
        }
    }
}
