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
    private float _tempTimeScale = 1;

    private Console Console
    {
        get
        {
            if (_console == null) _console = new Console(_prefix, _commands);
            return _console;
        }
    }

    private void OnEnable()
    {
        InputManager.RegisterCommand(Toggle);
    }

    public void Toggle(InputAction.CallbackContext context)
    {
        if (!context.action.triggered) return;

        if(_canvas.activeSelf)
        {
            Time.timeScale = _tempTimeScale;
            _canvas.SetActive(false);
        }
        else
        {
            _tempTimeScale = Time.timeScale;
            Time.timeScale = 0;
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
