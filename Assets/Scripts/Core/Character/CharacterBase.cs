using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBase : MonoBehaviour, ICharacter, IHealth
{
    public bool IsDebug = false;

    [Header("Data")]
    private string _name = "CharacterBase";
    private float _health = 100.0f;
    private float _maxHealth = 100.0f;
    private bool _imuttable = true;

    [Header("Movement")]
    protected StateMachine _stateMachine;
    [SerializeField] private Bounds _initBound;
    [SerializeField] private Vector2 _position;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private Vector2 _input;
    [SerializeField] private CharacterDirection _direction;
    [SerializeField] private bool _isGround = true;


    [Header("Reference")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _cameraTarget;

    public string Name { get { return _name; } }
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

    public Vector2 Input { get => _input; set => _input = value; }

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
    public bool IsGround => _isGround;


    public bool IsImuttable { get => _imuttable; }
    public float Health{ get => _health; }
    public float MaxHealth { get => _maxHealth; }
    public Animator Animator { get => _animator; }

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
        if (_health == 0) return;

        //업데이트
        _stateMachine.Update(this);
        UpdateRigidbodyVelocity();
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
        _health = Mathf.Clamp(_health - amount, 0f, _maxHealth);
        if (_health <= 0f)
        {
            Die(perp);
        }
    }

    public void RecoverFullHealth()
    {
        _health = _maxHealth;
    }

    public void Die(ICharacter perp)
    {
        //가해자, 사망원인 처리
    }

    #endregion

    private void UpdateRigidbodyVelocity()
    {
        if (_rb == null) return;
        //Debug.Log(_velocity);
        _rb.velocity = _velocity;
    }

}

public enum CharacterDirection
{
    Left, Right, None
}