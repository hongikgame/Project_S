using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase, IBreath
{
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
}
