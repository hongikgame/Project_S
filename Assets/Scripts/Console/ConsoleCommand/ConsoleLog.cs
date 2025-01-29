using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsoleLog", menuName = "Console/Log")]
public class ConsoleLog : ConsoleCommand
{
    public override bool Process(string[] args)
    {
        string text = string.Join("", args);

        Debug.Log(text);

        return true;
    }

}
