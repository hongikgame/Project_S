using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : StateBase
{
    private float _gravityScale = 0f;

    public AttackState(CharacterBase ownerCharacter) : base(ownerCharacter)
    {
    }

    public override void Start()
    {
        InputManager.AttackKeyDown = false;
        _ownerCharacter.Attack();

        _gravityScale = _ownerCharacter.Rigidbody2D.gravityScale;
        _ownerCharacter.Rigidbody2D.gravityScale = 0;
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
    }
}
