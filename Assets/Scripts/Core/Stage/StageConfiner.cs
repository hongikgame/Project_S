using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StageConfiner : MonoBehaviour
{
    private CinemachineBrain _cinemachineBrain;
    private CompositeCollider2D _compositeCollider;

    private void OnEnable()
    {
        _cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        _compositeCollider = GetComponent<CompositeCollider2D>();
    }

    private void OnDisable()
    {
        _cinemachineBrain = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (_cinemachineBrain == null || _cinemachineBrain.ActiveVirtualCamera == null || _compositeCollider == null) return;

            if (_cinemachineBrain.ActiveVirtualCamera is CinemachineVirtualCamera vcam)
            {
                CinemachineConfiner2D confiner = vcam.GetComponent<CinemachineConfiner2D>();

                if (confiner != null)
                {
                    confiner.m_BoundingShape2D = _compositeCollider;
                    //confiner.InvalidatePathCache();
                }
            }
        }
    }
}
