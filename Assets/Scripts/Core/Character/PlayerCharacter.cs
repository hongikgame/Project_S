using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase, IBreath
{
    [Header("Attack")]
    [SerializeField] private GameObject _attackRangeObject;
    [SerializeField] private float _orbitDistance;

    [Header("Breath")]
    private bool _canSpendOxygen = true;
    private float _maxOxygen = 100.0f;
    private float _oxygen = 100.0f;
    private float _oxygenSpendRate = 1f;
    private float _oxygenCPU = 2f;
    private WaitForSeconds _oxygenSpendWFS;
    private Coroutine _spendOxygenCoroutine;

    public bool CanSpendOxygen => _canSpendOxygen;
    public float MaxOxygen => _maxOxygen;
    public float Oxygen => _oxygen;

    private void Awake()
    {
        _stateMachine = new StateMachine();
        PlayerStateMachineData playerStateMachineData = new PlayerStateMachineData(this, 0);
        _stateMachine.ReplaceStateData(this, playerStateMachineData);

        _oxygenSpendWFS = new WaitForSeconds(1/_oxygenSpendRate);
        _spendOxygenCoroutine = StartCoroutine(SpendOxygen());
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

    public void OnDash(InputAction.CallbackContext context)
    {
        InputDash = context.ReadValue<bool>();
    }

    public void DecreaseOxygen(float amount)
    {
        Mathf.Clamp(_oxygen - amount, 0, _oxygen);
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
        yield return _oxygenSpendWFS;

        DecreaseOxygen(_oxygenCPU);
    }

    public override void Attack()
    {
        Vector2 aimPos = InputManager.AimPos;
        Vector2 characterPos = transform.position;

        Vector2 direction = aimPos - characterPos;

        float angle = Mathf.Atan2(direction.y, direction.x);
        Vector2 newPos = characterPos + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _orbitDistance;
        _attackRangeObject.transform.position = newPos;
        float rotationAngle = Mathf.Rad2Deg * angle - 90f;
        _attackRangeObject.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}
