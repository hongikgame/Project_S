using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsoleCommand
{
    string CommadWord { get; }
    bool Process(string[] args);
}
