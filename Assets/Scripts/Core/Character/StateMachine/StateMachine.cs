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
                return;
            }
        }

        Debug.Log("State 할당 불가, index0을 할당합니다.");
    }

    public void FixedUpdate(CharacterBase character)
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

    public void Update(CharacterBase character)
    {
        _currentState?.Update();
    }

    public string GetCurrentStateName()
    {
        return _currentState?.GetType().Name;
    }
}
