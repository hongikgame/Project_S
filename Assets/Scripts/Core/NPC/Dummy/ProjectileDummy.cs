using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileDummy : MonoBehaviour, IHealth
{
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private float _attackCooldownRemain = 2f;

    public bool IsImuttable => false;
    public float Health { get; set; } = 100f;
    public float MaxHealth => 100f;
    public float RecoverCooldown { get; private set; } = 3f;
    public float RecoverCooldownRemain { get; set; } = 3f;
    public float AttackCooldown { get => _attackCooldown; }
    public float AttackCooldownRemain { get => _attackCooldownRemain; }

    public Image HealthBar;
    public GameObject Projectile;
    public GameObject Target;

    private void Update()
    {
        if(RecoverCooldownRemain <= 0)
        {
            RecoverFullHealth();
            RecoverCooldownRemain = 0;
        }
        else RecoverCooldownRemain -= Time.deltaTime;

        if(_attackCooldownRemain <= 0)
        {
            _attackCooldownRemain = _attackCooldown;
            Attack();
        }
        else _attackCooldownRemain -= Time.deltaTime;
    }

    public void Die(ICharacter perp)
    {
        Destroy(this.gameObject);
    }

    public void GetDamage(ICharacter character, float amount)
    {
        Health = Mathf.Clamp(Health - amount, 0, Health);
        RecoverCooldownRemain = RecoverCooldown;
        UpdateHealthBar();

        if(Health <= 0) Die(character);
    }

    public void RecoverFullHealth()
    {
        Health = MaxHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float per = Health / MaxHealth;
        HealthBar.fillAmount = per;
    }

    public void Attack()
    {
        Vector3 targetPos = CharacterManager.GetCharacter("Player").GameObject.transform.position;
        Vector3 direction = (targetPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject go = Instantiate(Projectile, transform.position, Quaternion.Euler(0, 0, angle));
        ProjectileBase projectile = go.GetComponent<ProjectileBase>();

        projectile.Shooting(this.gameObject, direction);
    }
}
