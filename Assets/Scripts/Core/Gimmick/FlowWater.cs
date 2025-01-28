using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowWater : MonoBehaviour
{
    [SerializeField] private Vector2 _flowDirection = Vector2.zero;

    public Vector2 FlowDirection { get => _flowDirection; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerCharacterBase>(out PlayerCharacterBase characterBase))
        {
            characterBase.FlowWater = this;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerCharacterBase>(out PlayerCharacterBase characterBase))
        {
            if(characterBase.FlowWater == this) 
            { 
                characterBase.FlowWater = null; 
            }
        }
    }
}
