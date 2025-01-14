using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase, IBreath
{
    [Header("Attack")]
    [SerializeField] private SkillBase _fireSwordSkill;
    [SerializeField] private SkillBase _drillDashSkill;

    [Header("Breath")]
    [SerializeField] private bool _canSpendOxygen = true;
    [SerializeField] private float _maxOxygen = 100.0f;
    [SerializeField] private float _oxygen = 100.0f;
    [SerializeField] private float _oxygenSpendRate = 1f;
    [SerializeField] private float _oxygenCPU = 2f;
    private WaitForSeconds _oxygenSpendWFS;
    private Coroutine _spendOxygenCoroutine;

    public override bool IsOnWater
    {
        get => _isOnWater;
        set
        {
            _isOnWater = value;
            _rb.gravityScale = value ? 0 : _gravityScale;
            if(!value)
            {
                _oxygen = _maxOxygen;
                if (_spendOxygenCoroutine != null) StopCoroutine(_spendOxygenCoroutine);
            }
            else
            {
                if (_spendOxygenCoroutine != null) StopCoroutine(_spendOxygenCoroutine);
                _spendOxygenCoroutine = StartCoroutine(SpendOxygen());
            }
        }
    }
    public bool CanSpendOxygen => _canSpendOxygen;
    public float MaxOxygen => _maxOxygen;
    public float Oxygen => _oxygen;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        PlayerStateMachineData playerStateMachineData = new PlayerStateMachineData(this, 0);
        _stateMachine.ReplaceStateData(this, playerStateMachineData);

        _oxygenSpendWFS = new WaitForSeconds(1/_oxygenSpendRate);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        InputManager.RegisterPlayerMove(OnMove);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        InputManager.DeregisterPlayerMove(OnMove);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Input = context.ReadValue<Vector2>();
    }

    public override void Die(ICharacter perp = null)
    {
        _oxygen = _maxOxygen;
        _health = _maxHealth;
    }

    public void DecreaseOxygen(float amount)
    {
        _oxygen = Mathf.Clamp(_oxygen - amount, 0, _oxygen);
        if (_oxygen <= 0)
        {
            Die();
        }
    }

    public void IncreaseOxygen(float amount)
    {
        Mathf.Clamp(_oxygen + amount, _oxygen, _maxOxygen);
    }

    protected IEnumerator SpendOxygen()
    {
        while(true)
        {
            if (_isOnWater)
            {
                DecreaseOxygen(_oxygenCPU);
            }
            yield return _oxygenSpendWFS;
        }
    }

    public override void Attack()
    {
        base.Attack();

        Vector2 aimPos = Mouse.current.position.ReadValue();
        Vector2 aimWorldPos = Camera.main.ScreenToWorldPoint(aimPos);
        Vector2 characterPos = transform.position;
        
        if(aimWorldPos.x < characterPos.x) Direction = CharacterDirection.Left;
        else if (aimWorldPos.x > characterPos.x) Direction = CharacterDirection.Right;

        _fireSwordSkill.StartAttack(this, _attackDuration);
    }

    public override void Dash()
    {
        base.Dash();
        _drillDashSkill.StartAttack(this, _dashDuration);
    }
}
