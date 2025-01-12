using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public bool IsImuttable { get; }
    float Health { get; }
    float MaxHealth { get; }

    void GetDamage(ICharacter character, float amount);
    void RecoverFullHealth();
    void Die(ICharacter perp);
}