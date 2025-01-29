using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] private bool _canParrying = true;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _ownerObject;
    [SerializeField] private IHealth _ownerHealth;

    public bool CanParrying { get => _canParrying; }
    public GameObject OwnerObject { get => _ownerObject; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IHealth>(out IHealth iHealth))
        {
            if (iHealth == _ownerHealth)
            {
                return;
            }
            iHealth.GetDamage(null, _damage);
        }

        //Destroy(gameObject);
    }

    public void Shooting(GameObject OwnerObject, Vector2 direction)
    {
        _ownerObject = OwnerObject;
        if(_ownerObject.TryGetComponent<IHealth>(out IHealth ownerHealth))
        {
            _ownerHealth = ownerHealth;
        }

        _rb.linearVelocity = direction * _speed;
    }
}
