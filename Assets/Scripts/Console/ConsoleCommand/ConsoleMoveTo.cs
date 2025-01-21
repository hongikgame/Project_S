using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsoleMoveTo", menuName = "Console/MoveTo")]
public class ConsoleMoveTo : ConsoleCommand
{
    public override bool Process(string[] args)
    {
        CharacterData data = CharacterManager.GetCharacter(args[0]);
        if (data != null)
        {
            float x = 0;
            float y = 0;
            float z = 0;

            if (args.Length > 1) x = float.Parse(args[1]);
            if (args.Length > 2) y = float.Parse(args[2]);

            data.GameObject.transform.position = new Vector3(x, y, z);
        }
        return false;
    }
}
