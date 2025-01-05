using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private StateBase _currentState;
    private List<StateBase> _stateList;
    private List<StateLink> _stateLinkList;

    public void ReplaceStateData(CharacterBase characterBase, StateMachineData data)
    {
        _stateList = data.StateBase;
        _stateLinkList = data.StateLink;

        foreach (StateLink link in _stateLinkList)
        {
            if (link.CheckCondition(characterBase))
            {
                _currentState = link.NewState;
                break;
            }
        }
    }

    public void Update(CharacterBase character)
    {
        foreach (StateLink link in _stateLinkList)
        {
            if (link.OldState != _currentState) continue;
            if (link.CheckCondition(character))
            {
                _currentState.Finish();
                _currentState = link.NewState;
                _currentState.Start();
                break;
            }
        }

        _currentState.FixedUpdate();
    }
}
