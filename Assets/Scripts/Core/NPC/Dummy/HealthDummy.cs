using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDummy : MonoBehaviour, IHealth
{
    public bool IsImuttable => false;

    public float Health { get; set; } = 100f;

    public float MaxHealth => 100f;

    public float RecoverCooldown { get; private set; } = 3f;
    public float RecoverCooldownRemain { get; set; } = 3f;
    public bool IsTemporaryImuttable { get; set; }

    public Image HealthBar;

    private void Update()
    {
        if(RecoverCooldownRemain <= 0)
        {
            RecoverFullHealth();
            RecoverCooldownRemain = 0;
        }
        else RecoverCooldownRemain -= Time.deltaTime;
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
}
