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
        //_stateList.Add(new IdleState(owner));
        _stateList.Add(new SwimState(owner));
        _stateList.Add(new DashState(owner));
        _stateList.Add(new AttackState(owner));
    }

    protected override void GenerateLink(CharacterBase owner, int stage)
    {
        base.GenerateLink(owner, stage);

         _stateLinkList.Add(new StateLinkAdvance<SwimState, DashState>(_stateList, (o, l, d) => { return InputManager.DashKeyDown && d.DashCount > 0; }));
         _stateLinkList.Add(new StateLinkAdvance<DashState, SwimState>(_stateList, (o, l, d) => { return d.DashCooldownRemain <= 0; }));
         _stateLinkList.Add(new StateLinkAdvance<SwimState, AttackState>(_stateList, (o, l, d) => { return InputManager.AttackKeyDown; }));
         _stateLinkList.Add(new StateLinkAdvance<AttackState, SwimState>(_stateList, (o, l, d) => { return d.Input == Vector2.zero; }));
    }
}
