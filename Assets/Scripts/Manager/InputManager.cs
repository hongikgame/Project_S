using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    private static @GameInput _inputActions;

    public static bool AttackKeyDown { get; set; } = false;
    public static bool DashKeyDown { get; set; } = false;
    public static Vector2 AimPos { get; set; }

    static InputManager()
    {
        _inputActions = new @GameInput();
        _inputActions.Enable();

        _inputActions.Player.Attack.performed += OnPlayerAttack;
        _inputActions.Player.Attack.canceled += OnPlayerAttack;
        _inputActions.Player.Dash.performed += OnPlayerDash;
        _inputActions.Player.Dash.canceled += OnPlayerDash;
        _inputActions.Player.Aim.performed += OnPlayerAim;
    }

    public static void OnPlayerAttack(InputAction.CallbackContext context)
    {
        AttackKeyDown = context.ReadValueAsButton();
    }

    public static void OnPlayerDash(InputAction.CallbackContext context)
    {
        DashKeyDown = context.ReadValueAsButton();
    }

    public static void OnPlayerAim(InputAction.CallbackContext context)
    {
        AimPos = context.ReadValue<Vector2>();
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
