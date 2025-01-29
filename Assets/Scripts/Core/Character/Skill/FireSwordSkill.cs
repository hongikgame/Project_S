using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireSwordSkill : SkillBase
{
    public float OrbitDistance = 1f;
    public float ParryingDuration = 0.08f;
    public float AttackDuration = 0.20f;
    public float AttackDamage = 10f;

    private bool _isParrying = false;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if(AttackDuration < ParryingDuration) AttackDuration = ParryingDuration;
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IHealth>(out IHealth iHealth))
        {
            if (_ownerHealth == iHealth) Debug.Log("Attack myself");
            else iHealth.GetDamage(_ownerCharacter, AttackDamage);
        }

        if (_isParrying)
        {
            if (collision.TryGetComponent<ProjectileBase>(out ProjectileBase projectile))
            {
                if (projectile.CanParrying) ParryingProjectile(projectile);
            }
        }
    }

    public override void StartAttack(PlayerCharacterBase owner, float time)
    {
        base.StartAttack(owner, time);

        Vector2 aimPos = Mouse.current.position.ReadValue();
        Vector2 aimWorldPos = Camera.main.ScreenToWorldPoint(aimPos);
        Vector2 characterPos = _ownerCharacter.transform.position;
        Vector2 direction = aimWorldPos - characterPos;

        float angle = Mathf.Atan2(direction.y, direction.x);
        Vector2 newPos = characterPos + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * OrbitDistance;
        transform.position = newPos;
        float rotationAngle = Mathf.Rad2Deg * angle - 90f;
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

        StartCoroutine(AttackCoroutine());
    }

    protected override IEnumerator AttackCoroutine()
    {
        _collider.enabled = true;
        _isParrying = true;
        yield return new WaitForSeconds(ParryingDuration);

        _isParrying = false;
        yield return new WaitForSeconds(AttackDuration - ParryingDuration);

        _collider.enabled = false;
    }


    private void ParryingProjectile(ProjectileBase projectile)
    {
        Vector3 targetPos = projectile.OwnerObject.transform.position;
        Vector3 direction = (targetPos - transform.position).normalized;

        projectile.Shooting(gameObject, direction);
    }

}
