using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : StateBase
{
    public AttackState(CharacterBase ownerCharacter) : base(ownerCharacter)
    {
    }

    public override void Start()
    {
        InputManager.AttackKeyDown = false;
        _ownerCharacter.Attack();
    }
}
