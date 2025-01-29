using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineData
{
    protected List<StateBase> _stateList = new List<StateBase>();
    protected List<StateLink> _stateLinkList = new List<StateLink>();

    public List<StateBase> StateBase { get => _stateList; }
    public List<StateLink> StateLink { get => _stateLinkList; }

    public StateMachineData(PlayerCharacterBase owner, int stage)
    {
        GenerateState(owner, stage);
        GenerateLink(owner, stage);
    }

    protected virtual void GenerateState(PlayerCharacterBase owner, int stage)
    {
        _stateList.Clear();
    }

    protected virtual void GenerateLink(PlayerCharacterBase owner, int stage)
    {
        _stateLinkList.Clear();
    }
}
