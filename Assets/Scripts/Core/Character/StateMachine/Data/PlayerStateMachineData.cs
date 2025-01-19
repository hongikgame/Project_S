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
        _stateList.Add(new WalkState(owner));
        _stateList.Add(new JumpState(owner));
    }

    protected override void GenerateLink(CharacterBase owner, int stage)
    {
        base.GenerateLink(owner, stage);

         _stateLinkList.Add(new StateLinkAdvance<NoneState, SwimState>(_stateList, (o, l, d) => { return true; }));
         _stateLinkList.Add(new StateLinkAdvance<SwimState, DashState>(_stateList, (o, l, d) => { return d.CanDash && InputManager.DashKeyDown; }));
         _stateLinkList.Add(new StateLinkAdvance<DashState, SwimState>(_stateList, (o, l, d) => { return d.DashCooldownRemain <= 0; }));
         _stateLinkList.Add(new StateLinkAdvance<SwimState, AttackState>(_stateList, (o, l, d) => { return d.CanAttack && InputManager.AttackKeyDown; }));
         _stateLinkList.Add(new StateLinkAdvance<AttackState, SwimState>(_stateList, (o, l, d) => { return d.AttackCooldownRemain <= 0; }));
         _stateLinkList.Add(new StateLinkAdvance<SwimState, WalkState>(_stateList, (o, l, d) => { return d.CurrentDetectorData.IsGround; }));
         _stateLinkList.Add(new StateLinkAdvance<WalkState, SwimState>(_stateList, (o, l, d) => { return !d.CurrentDetectorData.IsGround && d.IsOnWater; }));
         _stateLinkList.Add(new StateLinkAdvance<WalkState, JumpState>(_stateList, (o, l, d) => { return d.CurrentDetectorData.IsGround && InputManager.DashKeyDown; }));
         _stateLinkList.Add(new StateLinkAdvance<JumpState, WalkState>(_stateList, (o, l, d) => { return d.CurrentDetectorData.IsGround; }));
         _stateLinkList.Add(new StateLinkAdvance<JumpState, SwimState>(_stateList, (o, l, d) => { return !d.CurrentDetectorData.IsGround && d.IsOnWater; }));
    }
}
