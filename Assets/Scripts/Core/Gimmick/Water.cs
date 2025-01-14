using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger entered by: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player detected in trigger.");
            CharacterBase cb = collision.gameObject.GetComponent<CharacterBase>();
            if (cb != null)
            {
                //Debug.Log("PlayerCharacter component found, setting IsOnWater to true.");
                cb.IsOnWater = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterBase cb = collision.gameObject.GetComponent<CharacterBase>();
            if (cb != null)
            {
                cb.IsOnWater = false;
            }
        }
    }
}
