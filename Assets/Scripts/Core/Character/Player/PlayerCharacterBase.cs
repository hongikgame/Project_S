using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerCharacterBase : CharacterBase, IBreath
{
    [Header("Reference")]
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Transform _cameraTarget;

    [Header("Breath")]
    [SerializeField] protected bool _canSpendOxygen = true;
    [SerializeField] protected float _maxOxygen = 100.0f;
    [SerializeField] protected float _oxygen = 100.0f;
    [SerializeField] protected float _spendOxygenInSecond = 2;

    [Header("StateMachine")]
    [SerializeField] private DetectorData _currentData;
    [SerializeField] private DetectorData _prevData;
    protected StateMachine _stateMachine;
    protected List<DetectorBase> _detectorList = new List<DetectorBase>();
    protected DetectorStaticData _staticData;

    [Header("Movement")]
    [SerializeField] protected bool _isInfluenceByFlowWater = false;
    [SerializeField] protected Bounds _initBound;
    [SerializeField] protected Vector2 _position;
    [SerializeField] protected Vector2 _velocity;
    [SerializeField] protected float _gravityScale;
    [SerializeField] protected Vector2 _inputMove;
    [SerializeField] protected FlowWater _flowWater;
    [SerializeField] protected CharacterDirection _prevDirection;
    [SerializeField] protected CharacterDirection _direction;

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

    //Getter or Setter

    #region IBreath
    public bool CanSpendOxygen { get => _canSpendOxygen; }
    public float MaxOxygen { get => _maxOxygen; }
    public float Oxygen
    {
        get => _oxygen;
        set
        {
            _oxygen = value;
            _oxygen = Mathf.Clamp(_oxygen, 0, _maxOxygen);

            if (_oxygen <= 0)
            {
                OnOxygenDepleted();
            }
            else
            {
                OnOxygenRestored();
            }
        }
    }
    public float SpendOxygenInSecond { get => _spendOxygenInSecond; }
    #endregion

    #region StateMachine
    public StateMachine StateMachine { get { return _stateMachine; } }
    public DetectorData CurrentDetectorData { get => _currentData; }
    #endregion

    #region Movement

    public bool IsInfluenceByFlowWater { get => _isInfluenceByFlowWater; set => _isInfluenceByFlowWater = value; }
    public Vector2 Velocity
    {
        get => _velocity;
        set
        {
            _velocity = value;
            //if (Mathf.Approximately(value.x, 0)) return;
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
            Vector3 currentRotation = transform.rotation.eulerAngles;
            switch (value)
            {
                case CharacterDirection.Left:
                    _spriteRenderer.flipX = true;
                    //gameObject.transform.rotation = Quaternion.Euler(0, 180, currentRotation.z);
                    if (_cameraTarget) _cameraTarget.transform.localPosition = new Vector3(-1f, 0f, 0f);
                    break;
                case CharacterDirection.Right:
                    _spriteRenderer.flipX = false;
                    //gameObject.transform.rotation = Quaternion.Euler(0, 0, currentRotation.z);
                    if (_cameraTarget) _cameraTarget.transform.localPosition = new Vector3(1f, 0f, 0f);
                    break;
                case CharacterDirection.None:
                    break;
            }
            _direction = value;
        }
    }
    public CharacterDirection PrevDirection { get => _prevDirection; set => _prevDirection = value;}
    #endregion

    #region Skill
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
    #endregion

    #region Reference
    public Animator Animator { get => _animator; }
    public Rigidbody2D Rigidbody2D { get => _rb; }
    #endregion


    //Func
    #region Monobehaviour

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();

        _initBound = _collider.bounds;

        _staticData = new DetectorStaticData();
        _currentData = new DetectorData();
        _prevData = new DetectorData();
        _staticData.Bounds = _initBound;
    }

    protected virtual void FixedUpdate()
    {
        UpdateDetector();
        _velocity = _rb.linearVelocity;

        _stateMachine.FixedUpdate(this);
        UpdateOxygen();
        UpdateRigidbodyVelocity();
    }

    protected virtual void Update()
    {
        //UpdateCharacterRotation();
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


    #region IHealth
    public override void Die(ICharacter perp = null)
    {
        
    }

    #endregion

    #region IBreath
    public virtual void RecoverFullOxygen()
    {
        Oxygen = MaxOxygen;
    }

    public virtual void OnOxygenDepleted()
    {

    }

    public virtual void OnOxygenRestored()
    {

    }
    #endregion

    private void UpdateRigidbodyVelocity()
    {
        if (_rb == null) return;
        
        _rb.linearVelocity = _velocity;
    }

    private void UpdateCharacterRotation()
    {
        if (_inputMove == Vector2.zero) return;

        float angle = Utils.GetAngle(_inputMove);
        if (angle > 90f)
        {
            angle = -(angle - 180);
        }
        else if(angle < -90f)
        {
            angle = -(angle + 180);
        }

        /*CharacterRotation rotaion;
        if (angle > 75f)
            rotaion = CharacterRotation.Up;
        else if (angle >= 15f)
            rotaion = CharacterRotation.UpSide;
        else if (angle >= -15f)
            rotaion = CharacterRotation.Side;
        else if (angle >= -75f)
            rotaion = CharacterRotation.DownSide;
        else
            rotaion = CharacterRotation.Down;*/

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        if (_inputMove.x < 0)
        {
            targetRotation = Quaternion.Euler(0, 0, -angle);
        }

        if(_prevDirection != _direction)
        {
            transform.rotation = targetRotation;
        }
        else if (Mathf.Abs(transform.rotation.y - angle) < 45f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }
        else
        {
            transform.rotation = targetRotation;
        }

        _prevDirection = _direction;
        //_rotation = rotaion;
    }

    private void UpdateDetector()
    {
        _prevData = _currentData.DeepCopy;
        _currentData = new DetectorData();
        _staticData.Bounds = _collider.bounds;

        foreach(DetectorBase detector in _detectorList)
        {
            detector.UpdateDetector(_currentData, _staticData);
        }
    }

    private void UpdateOxygen()
    {
        if(_currentData.IsOnWater)
        {
            Oxygen -= _spendOxygenInSecond * Time.fixedDeltaTime;
        }
        else
        {
            Oxygen = _maxOxygen;
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

public enum CharacterRotation
{
    Up,
    UpSide,
    Side,
    DownSide,
    Down
}
