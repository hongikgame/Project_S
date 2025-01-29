using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OxygenBubble : MonoBehaviour
{
    [SerializeField] private float _oxygenIncreaseRate;

    private List<IBreath> _iBreathList = new List<IBreath>();

    private void OnEnable()
    {
        StartCoroutine(GiveOxygenCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(GiveOxygenCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IBreath>(out IBreath iBreath))
        {
            _iBreathList.Add(iBreath);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IBreath>(out IBreath iBreath))
        {
            _iBreathList.Remove(iBreath);
        }
    }

    private IEnumerator GiveOxygenCoroutine()
    {
        foreach(IBreath iBreath in  _iBreathList)
        {
            iBreath.Oxygen = iBreath.Oxygen + _oxygenIncreaseRate / Time.deltaTime;
        }
        
        yield return null;
    }
}
