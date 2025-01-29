using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(fileName = "ConsoleShowDebug", menuName = "Console/ShowDebug")]
public class ConsoleShowDebug : ConsoleCommand
{
    public override bool Process(string[] args)
    {
        UI_Debug.Instance.DebugToggle();
        return true;
    }
}
