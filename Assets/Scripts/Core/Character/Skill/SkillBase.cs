using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public abstract void StartAttack(CharacterBase owner);
    public abstract void FinishAttack();
}
