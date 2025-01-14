using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterBase : MonoBehaviour, ICharacter, IHealth
{
    [Header("Data")]
    [SerializeField] protected string _name = "CharacterBase";

    [Header("Health")]
    [SerializeField] protected float _health = 100.0f;
    [SerializeField] protected float _maxHealth = 100.0f;
    [SerializeField] protected bool _imuttable = false;
    [SerializeField] protected bool _temporaryImuttable = false;

    [Header("Movement")]
    [SerializeField] protected bool _isInfluenceByFlowWater = false;
    [SerializeField] protected bool _isOnWater = false;
    [SerializeField] protected Bounds _initBound;
    [SerializeField] protected Vector2 _position;
    [SerializeField] protected Vector2 _velocity;
    [SerializeField] protected float _gravityScale;
    [SerializeField] protected Vector2 _inputMove;
    [SerializeField] protected FlowWater _flowWater;
    [SerializeField] protected CharacterDirection _direction;
    [SerializeField] protected bool _isGround = true;
    protected StateMachine _stateMachine;

    [Header("Cooldown - Dash")]
    [SerializeField] private int _maxDashCount = 3;
    [SerializeField] private int _dashCount = 3;
    [SerializeField] private float _dashCooldown = 3f;
    [SerializeField] private float _dashCooldownRemain = 0f;
    [SerializeField] protected float _dashDuration = 0.25f;
    [SerializeField] private float _dashDurationRemain = 0f;

    [Header("Cooldown - Attack")]
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _attackCooldownRemain = 0f;
    [SerializeField] protected float _attackDuration = 0.35f;
    [SerializeField] private float _attackDurationRemain = 0f;

    [Header("Reference")]
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Transform _cameraTarget;

    public string Name { get { return _name; } }
    public StateMachine StateMachine { get { return _stateMachine; } }
    public Vector2 Velocity
    {
        get => _velocity;
        set
        {
            _velocity = value;
            if (Mathf.Approximately(value.x, 0)) return;
            if (_velocity.x > 0.1) Direction = CharacterDirection.Right;
            else if (_velocity.x < -0.1) Direction = CharacterDirection.Left;
        }
    }
    public Vector2 Input { get => _inputMove; set => _inputMove = value; }
    public FlowWater FlowWater { get => _flowWater; set => _flowWater = value; }
    public CharacterDirection Direction
    {
        get => _direction;
        set
        {
            switch (value)
            {
                case CharacterDirection.Left:
                    _spriteRenderer.flipX = true;
                    if (_cameraTarget) _cameraTarget.transform.localPosition = new Vector3(-1f, 0f, 0f);
                    break;
                case CharacterDirection.Right:
                    _spriteRenderer.flipX = false;
                    if (_cameraTarget) _cameraTarget.transform.localPosition = new Vector3(1f, 0f, 0f);
                    break;
                case CharacterDirection.None:
                    break;
            }
            _direction = value;
        }
    }
    public bool IsInfluenceByFlowWater { get => _isInfluenceByFlowWater; set => _isInfluenceByFlowWater = value; }
    public virtual bool IsOnWater 
    { 
        get => _isOnWater; 
        set
        {
            _isOnWater = value;
            _rb.gravityScale = value ? 0 : _gravityScale;
        }
    }
    public bool IsGround { get => _isGround; set => _isGround = value; }
    public bool CanDash
    { 
        get
        {
            return _dashCount > 0;
        } 
    }
    public int DashCount { get => _dashCount; }
    public int MaxDashCount { get => _maxDashCount; }
    public float DashCooldownRemain
    {
        get => _dashDurationRemain;
        set
        {
            _dashDurationRemain = value;
            _dashDurationRemain = Mathf.Clamp(_dashDurationRemain, 0, _dashDuration);
        }
    }
    public bool CanAttack { get => _attackCooldownRemain <= 0; }
    public float AttackCooldownRemain
    { 
        get => _attackDurationRemain; 
        set
        {
            _attackDurationRemain = value;
            _attackDurationRemain = Mathf.Clamp(_attackDurationRemain, 0, _attackDuration);
        }
    }
    public bool IsImuttable { get => _imuttable; }
    public bool IsTemporaryImuttable { get => _temporaryImuttable; set => _temporaryImuttable = value; }
    public float Health{ get => _health; }
    public float MaxHealth { get => _maxHealth; }
    public Animator Animator { get => _animator; }
    public Rigidbody2D Rigidbody2D { get => _rb; }

    #region Monobehaviour

    protected virtual void OnEnable()
    {
        RegisterNPC();
    }

    protected virtual void OnDisable()
    {
        DeregisterNPC();
    }

    private void FixedUpdate()
    {
        //죽었으면 리턴
        //if (_health == 0) return;

        //업데이트
        _velocity = Vector2.zero;
        _stateMachine.FixedUpdate(this);
        UpdateRigidbodyVelocity();
        //UpdateZRotation();
    }

    protected virtual void Update()
    {
        _stateMachine.Update(this);

        //Attack
        if(_attackCooldownRemain > 0) _attackCooldownRemain -= Time.deltaTime;
        else _attackCooldownRemain = 0;

        //Dash
        if (_dashCooldownRemain > 0) _dashCooldownRemain -= Time.deltaTime;
        else
        {
            _dashCooldownRemain = 0;
            if(_dashCount < _maxDashCount)
            {
                _dashCount++;
                _dashCooldownRemain = _dashCooldown;
            }
        }
    }
    #endregion

    #region ICharacter

    public void RegisterNPC()
    {
        CharacterManager.RegisterCharacter(gameObject, this);
    }

    public void DeregisterNPC()
    {
        CharacterManager.DeregisterCharacter(gameObject, this);
    }

    #endregion

    #region IHealth
    public void GetDamage(ICharacter perp, float amount)
    {
        if(!_imuttable && !IsTemporaryImuttable)
        {
            _health = Mathf.Clamp(_health - amount, 0f, _maxHealth);
            if (_health <= 0f)
            {
                Die(perp);
            }
        }
    }

    public void RecoverFullHealth()
    {
        _health = _maxHealth;
    }

    public virtual void Die(ICharacter perp = null)
    {
        //가해자, 사망원인 처리
    }

    #endregion

    private void UpdateRigidbodyVelocity()
    {
        if (_rb == null) return;
        
        if(_isInfluenceByFlowWater)
        {
            if (_flowWater) 
            { 
                _velocity += _flowWater.FlowDirection;
            }
        }
        
        _rb.velocity = _velocity;
    }

    private void UpdateZRotation()
    {
        if (_inputMove == Vector2.zero) return;
        if (_isInfluenceByFlowWater)
        {
            float angle = Mathf.Atan2(_inputMove.y, _inputMove.x) * Mathf.Rad2Deg;
            if (angle > 90)
            {
                angle -= 180;
            }
            else if (angle < -90)
            {
                angle += 180;
            }
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
    }

    public virtual void Attack()
    {
        _attackCooldownRemain = _attackCooldown;
    }

    public virtual void Dash()
    {
        _dashCount --;
        _dashCooldownRemain = _dashCooldown;
    }

}

public enum CharacterDirection
{
    Left, Right, None
}