using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineData : StateMachineData
{
    public PlayerStateMachineData(CharacterBase owner, int stage) : base(owner, stage)
    {
        
    }

    protected override void GenerateState(CharacterBase owner, int stage)
    {
        base.GenerateState(owner, stage);
        _stateList.Add(new IdleState(owner));
        _stateList.Add(new WalkState(owner));
        //_stateList.Add(new G_JumpState(owner));
    }

    protected override void GenerateLink(CharacterBase owner, int stage)
    {
        base.GenerateLink(owner, stage);

        _stateLinkList.Add(new StateLinkAdvance<IdleState, WalkState>(_stateList, (o, l, d) => { return d.IsGround && d.Input != Vector2.zero; }));
        _stateLinkList.Add(new StateLinkAdvance<WalkState, IdleState>(_stateList, (o, l, d) => { return d.IsGround && d.Input == Vector2.zero; }));
    }
}
