using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineData
{
    protected List<StateBase> _stateList = new List<StateBase>();
    protected List<StateLink> _stateLinkList = new List<StateLink>();

    public List<StateBase> StateBase { get => _stateList; }
    public List<StateLink> StateLink { get => _stateLinkList; }

    public StateMachineData(CharacterBase owner, int stage)
    {
        GenerateState(owner, stage);
        GenerateLink(owner, stage);
    }

    protected virtual void GenerateState(CharacterBase owner, int stage)
    {
        _stateList.Clear();
    }

    protected virtual void GenerateLink(CharacterBase owner, int stage)
    {
        _stateLinkList.Clear();
    }
}
