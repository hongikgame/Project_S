using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Console
{
    private readonly string _prefix;
    private readonly IEnumerable<IConsoleCommand> _commands;

    public Console(string prefix, IEnumerable<IConsoleCommand> commands)
    {
        _prefix = prefix;
        _commands = commands;
    }

    public void ProcessCommand(string input, string[] args)
    {
        foreach (var command in _commands)
        {
            if (!input.Equals(command.CommadWord, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }
            if (command.Process(args))
            {
                return;
            }
        }
    }

    public void ProcessCommand(string input)
    {
        if (!input.StartsWith(_prefix)) return;
        
        input = input.Remove(0, _prefix.Length);
        string[] split = input.Split(' ');

        string commandPrefix = split[0];
        string[] commandArgs = split.Skip(1).ToArray();

        ProcessCommand(commandPrefix, commandArgs);
    }
}
