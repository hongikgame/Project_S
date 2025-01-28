using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private CompositeCollider2D _compositeCollider2D;
    [SerializeField] private StageChanger _stageChanger;
    [SerializeField] private Sprite _backgroundSprite;

    public int Index { get => _index; }
    public CompositeCollider2D CameraConfiner2D { get => _compositeCollider2D; }

    private void Awake()
    {
        if(_stageChanger) _stageChanger.Index = _index;
    }

    private void Start()
    {
        StageManager.RegisterStage(this);
    }

    public Vector3 GetSpawnPoint()
    {
        return _spawnPoint.transform.position;
    }
}
