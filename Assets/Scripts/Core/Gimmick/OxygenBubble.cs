using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OxygenBubble : MonoBehaviour
{
    [SerializeField] private float _oxygenAmount;

    public float OxygenAmount => _oxygenAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IBreath>(out IBreath iBreath))
        {
            iBreath.IncreaseOxygen(OxygenAmount);
        }
    }
}
