using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDashSkill : SkillBase
{
    public float AttackDamage = 30f;

    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private bool _ownerCharacterIsImmutable = false;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IHealth>(out IHealth iHealth))
        {
            if(iHealth != _ownerHealth)
            {
                iHealth.GetDamage(_ownerCharacter, AttackDamage);
            }

            if(collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.AddForce(_ownerCharacter.Velocity);
            }
        }
    }

    public override void StartAttack(CharacterBase owner, float time)
    {
        base.StartAttack(owner, time);

        _collider.enabled = true;

        if(_ownerHealth != null)
        {
            _ownerHealth.IsTemporaryImuttable = true;
        }
        
        if (GameManager.Instance.Debug) _spriteRenderer.color = new Color(1, 0, 0, 1);
    }

    protected override void FinishAttack()
    {
        _collider.enabled = false;

        if (_ownerHealth != null)
        {
            _ownerHealth.IsTemporaryImuttable = false;
        }

        if (GameManager.Instance.Debug) _spriteRenderer.color = new Color(1, 0, 0, 0);
    }

    protected override IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(_time);
        FinishAttack();
    }
}
