using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StateLinkAdvance<Told, Tnew> : StateLink where Told : StateBase where Tnew : StateBase
{
    private readonly Told _oldState;
    private readonly Tnew _newState;
    private readonly Func<Told, Tnew, PlayerCharacterBase, bool> _checkCondition;

    public override StateBase OldState => _oldState;

    public override StateBase NewState => _newState;

    public StateLinkAdvance(IEnumerable states, Func<Told, Tnew, PlayerCharacterBase, bool> condition)
    {
        _oldState = states.OfType<Told>().FirstOrDefault<Told>();
        _newState = states.OfType<Tnew>().FirstOrDefault<Tnew>();
        _checkCondition = condition;
    }

    public override bool CheckCondition(PlayerCharacterBase character)
    {
        return this._checkCondition(_oldState, _newState, character);
    }
}
