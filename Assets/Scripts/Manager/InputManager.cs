using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    private static @GameInput _inputActions;

    private static bool _attackKeyDown = false;
    private static bool _dashKeyDown = false;

    public static bool AttackKeyDown { get; set; } = false;
    public static bool DashKeyDown
    { 
        get
        {
            if(_dashKeyDown)
            {
                _dashKeyDown = false;
                return true;
            }
            return false;
        }
        set => _dashKeyDown = value;
    }

    static InputManager()
    {
        _inputActions = new @GameInput();
        _inputActions.Enable();

        _inputActions.Player.Attack.performed += OnPlayerAttack;
        _inputActions.Player.Attack.canceled += OnPlayerAttack;
        _inputActions.Player.Dash.performed += OnPlayerDash;
        _inputActions.Player.Dash.canceled += OnPlayerDash;
    }

    public static void OnPlayerAttack(InputAction.CallbackContext context)
    {
        AttackKeyDown = context.ReadValueAsButton();
    }

    public static void OnPlayerDash(InputAction.CallbackContext context)
    {
        DashKeyDown = context.ReadValueAsButton();
    }

    public static void SetPlayerInputActivate(bool active)
    {
        if(active) _inputActions.Player.Enable();
        else _inputActions.Player.Disable();
    }

    public static void RegisterPlayerMove(Action<InputAction.CallbackContext> action)
    {
        _inputActions.Player.Move.performed -= action;
        _inputActions.Player.Move.performed += action; 
        _inputActions.Player.Move.canceled -= action;
        _inputActions.Player.Move.canceled += action;
    }

    public static void DeregisterPlayerMove(Action<InputAction.CallbackContext> action)
    {
        _inputActions.Player.Move.performed -= action;
        _inputActions.Player.Move.canceled -= action;
    }
}
