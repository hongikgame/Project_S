using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StageChanger : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;

    [SerializeField] private CompositeCollider2D _prevStageConfiner;
    [SerializeField] private CompositeCollider2D _nextStageConfiner;

    [SerializeField] private Stage _prevStage;
    [SerializeField] private Stage _nextStage;
    [SerializeField] private StageChanger _prevStageChanger;
    [SerializeField] private StageChanger _nextStageChanger;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        SetTrigger(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CinemachineBrain cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
            if (cinemachineBrain == null || cinemachineBrain.ActiveVirtualCamera == null || _nextStageConfiner == null) return;

            if (cinemachineBrain.ActiveVirtualCamera is CinemachineVirtualCamera vcam)
            {
                CinemachineConfiner confiner = vcam.GetComponent<CinemachineConfiner>();

                if (confiner != null)
                {
                    confiner.m_BoundingShape2D = _nextStageConfiner;

                    StageManager.SetStage(_nextStage);

                    SetTrigger(false);
                    _prevStageChanger?.SetTrigger(false);
                    _nextStageChanger?.SetTrigger(true);
                }
            }
        }
    }

    public void SetTrigger(bool b)
    {
        _collider.isTrigger = b;
    }
}
