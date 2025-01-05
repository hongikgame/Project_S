using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager
{
    private static @GameInput _inputActions;

    static InputManager()
    {
        _inputActions = new @GameInput();
        _inputActions.Enable();
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
