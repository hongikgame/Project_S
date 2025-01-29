using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConsoleBehaviour : SingletonMonobehavior<ConsoleBehaviour>
{
    [SerializeField] private string _prefix = string.Empty;
    [SerializeField] private ConsoleCommand[] _commands = new ConsoleCommand[0];

    [Header("UI")]
    [SerializeField] private GameObject _canvas;
    [SerializeField] private TMP_InputField _InputField;

    private static Console _console;

    private Console Console
    {
        get
        {
            if (_console == null) _console = new Console(_prefix, _commands);
            return _console;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _InputField.onEndEdit.AddListener(ProcessCommand);
    }

    private void OnEnable()
    {
        InputManager.RegisterCommand(Toggle);
    }

    private void OnDisable()
    {
        InputManager.DeregisterCommand(Toggle);
    }

    public void Toggle(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) return;

        if(_canvas.activeSelf)
        {
            InputManager.SetPlayerInputActivate(true);
            _canvas.SetActive(false);
        }
        else
        {
            InputManager.SetPlayerInputActivate(false);
            _canvas.SetActive(true);
            _InputField.ActivateInputField();
        }
    }

    public void ProcessCommand(string input)
    {
        Console.ProcessCommand(input);
        _InputField.text = string.Empty;
    }
}
