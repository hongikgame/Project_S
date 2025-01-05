using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public string Name { get; }


    void RegisterNPC();

    void DeregisterNPC();
}
