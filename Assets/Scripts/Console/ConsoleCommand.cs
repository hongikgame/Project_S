using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
{
    [SerializeField] private string _commandWord;
    public string CommadWord => _commandWord;

    public abstract bool Process(string[] args);
}
