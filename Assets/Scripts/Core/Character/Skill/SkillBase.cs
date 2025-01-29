using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    protected PlayerCharacterBase _ownerCharacter = null;
    protected IHealth _ownerHealth = null;
    protected float _time;
    protected Coroutine _attackCoroutine;

    public virtual void StartAttack(PlayerCharacterBase owner, float time)
    {
        if (_ownerCharacter == null)
        {
            _ownerCharacter = owner;
            _ownerHealth = owner.GetComponent<IHealth>();
        }

        _time = time;
        if (_attackCoroutine != null) StopCoroutine(AttackCoroutine());
        _attackCoroutine = StartCoroutine(AttackCoroutine());
    }
    protected virtual void FinishAttack()
    {

    }

    protected virtual IEnumerator AttackCoroutine()
    {
        yield return null;
    }
}
