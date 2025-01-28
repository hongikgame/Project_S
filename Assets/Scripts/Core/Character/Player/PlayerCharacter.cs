using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : PlayerCharacterBase
{
    [Header("Attack")]
    [SerializeField] private SkillBase _fireSwordSkill;
    [SerializeField] private SkillBase _drillDashSkill;

    protected override void Awake()
    {
        base.Awake();

        _stateMachine = new StateMachine();
        PlayerStateMachineData playerStateMachineData = new PlayerStateMachineData(this, 0);
        _stateMachine.ReplaceStateData(this, playerStateMachineData);
        
        _detectorList.Add(new GroundDetector());
        _detectorList.Add(new WaterDetector());
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

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Input = context.ReadValue<Vector2>();
    }

    public override void NoOxygen()
    {
        
    }

    public override void Die(ICharacter perp = null)
    {
        _oxygen = _maxOxygen;
        _health = _maxHealth;
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

    public override void GetDamage(ICharacter perp, float amount)
    {
        base.GetDamage(perp, amount);
        EventHandler.CallPlayerGetDamage(_health, _maxHealth);
    }
}
