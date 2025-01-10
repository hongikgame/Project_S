using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowWater : MonoBehaviour
{
    [SerializeField] private Vector2 _flowDirection = Vector2.zero;

    public Vector2 FlowDirection { get => _flowDirection; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<CharacterBase>(out CharacterBase characterBase))
        {
            characterBase.FlowWater = this;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<CharacterBase>(out CharacterBase characterBase))
        {
            if(characterBase.FlowWater == this) 
            { 
                characterBase.FlowWater = null; 
            }
        }
    }
}
