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

    public bool CanParrying { get => _canParrying; }
    public GameObject OwnerObject { get => _ownerObject; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.Log(name + " velocity: " + _rb.velocity.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IHealth>(out IHealth iHealth))
        {
            iHealth.GetDamage(null, _damage);
        }

        //Destroy(gameObject);
    }

    public void Shooting(GameObject OwnerObject, Vector2 direction)
    {
        _ownerObject = OwnerObject;

        _rb.velocity = direction * _speed;
    }
}
