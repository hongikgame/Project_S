using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase
{
    private void Awake()
    {
        _stateMachine = new StateMachine();
        PlayerStateMachineData playerStateMachineData = new PlayerStateMachineData(this, 0);
        _stateMachine.ReplaceStateData(this, playerStateMachineData);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        InputManager.RegisterPlayerMove(OnMove);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        InputManager.DeregisterPlayerMove(OnMove);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Input = context.ReadValue<Vector2>();
    }
}
