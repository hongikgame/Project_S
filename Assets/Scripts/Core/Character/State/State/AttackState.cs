using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackState : StateBase
{
    private float _gravityScale = 0f;
    private Vector2 _velocity;
    public AttackState(PlayerCharacterBase ownerCharacter) : base(ownerCharacter)
    {
        _animationHash = Animator.StringToHash("Swim_Attack");
    }

    public override void Start()
    {
        InputManager.AttackKeyDown = false;
        _ownerCharacter.Attack();

        _gravityScale = _ownerCharacter.Rigidbody2D.gravityScale;
        _ownerCharacter.Rigidbody2D.gravityScale = 0;

        _velocity = _ownerCharacter.Velocity;
        _ownerCharacter.Velocity = Vector2.zero;

        _ownerCharacter.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();


        _ownerCharacter.Velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();

        _ownerCharacter.AttackCooldownRemain = _ownerCharacter.AttackCooldownRemain - Time.deltaTime;
    }

    public override void Finish()
    {
        base.Finish();

        _ownerCharacter.AttackCooldownRemain = 99999f;
        _ownerCharacter.Rigidbody2D.gravityScale = _gravityScale;
        _ownerCharacter.Velocity = _velocity;
    }

    public override void UpdateAnimation()
    {
        PlayAnimation(_animationHash);
    }
}
