using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireSwordSkill : SkillBase
{
    public CharacterBase OwnerCharacter = null;
    public float OrbitDistance = 1f;
    public float ParryingDuration = 0.08f;
    public float AttackDuration = 0.20f;
    public float AttackDamage = 10f;

    private bool _isParrying = false;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private WaitForSeconds _delay;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _delay = new WaitForSeconds(AttackDuration);

        if(AttackDuration < ParryingDuration) AttackDuration = ParryingDuration;
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IHealth>(out IHealth iHealth))
        {
            if (OwnerCharacter == iHealth) Debug.Log("Attack myself");
            else iHealth.GetDamage(OwnerCharacter, AttackDamage);
        }

        if (_isParrying)
        {
            if (collision.TryGetComponent<ProjectileBase>(out ProjectileBase projectile))
            {
                if (projectile.CanParrying) ParryingProjectile(projectile);
            }
        }
    }

    public override void StartAttack(CharacterBase owner)
    {
        OwnerCharacter = owner;

        Vector2 aimPos = Mouse.current.position.ReadValue();
        Vector2 aimWorldPos = Camera.main.ScreenToWorldPoint(aimPos);
        Vector2 characterPos = OwnerCharacter.transform.position;
        Vector2 direction = aimWorldPos - characterPos;

        float angle = Mathf.Atan2(direction.y, direction.x);
        Vector2 newPos = characterPos + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * OrbitDistance;
        transform.position = newPos;
        float rotationAngle = Mathf.Rad2Deg * angle - 90f;
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

        if(GameManager.Instance.Debug) _spriteRenderer.color = new Color(1, 1, 1, 1);
        else _spriteRenderer.color = new Color(1, 1, 1, 0);

        StartCoroutine(AttackCoroutine());
    }


    public override void FinishAttack()
    {
        
    }

    private IEnumerator AttackCoroutine()
    {
        _collider.enabled = true;
        _isParrying = true;
        if(GameManager.Instance.Debug) _spriteRenderer.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(ParryingDuration);

        _isParrying = false;
        yield return new WaitForSeconds(AttackDuration - ParryingDuration);

        _collider.enabled = false;
        _spriteRenderer.color = new Color(1, 1, 1, 0);
    }


    private void ParryingProjectile(ProjectileBase projectile)
    {
        Vector3 targetPos = projectile.OwnerObject.transform.position;
        Vector3 direction = (targetPos - transform.position).normalized;

        projectile.Shooting(gameObject, direction);
    }

}
