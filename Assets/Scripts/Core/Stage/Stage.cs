using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private int _index;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private CompositeCollider2D _compositeCollider2D;
    [SerializeField] private GameObject _cameraParent;
    [SerializeField] private StageChanger _stageChanger;
    [SerializeField] private Sprite _backgroundSprite;

    private List<CinemachineVirtualCamera> _vCamList = new List<CinemachineVirtualCamera>();

    public int Index { get => _index; }
    public CompositeCollider2D CameraConfiner2D { get => _compositeCollider2D; }
    public List<CinemachineVirtualCamera> VirtualCameraList { get => _vCamList; }

    private void Awake()
    {
        if(_stageChanger) _stageChanger.Index = _index;
        foreach(Transform child in _cameraParent.transform)
        {
            if(child.TryGetComponent<CinemachineVirtualCamera>(out CinemachineVirtualCamera vCam))
            {
                _vCamList.Add(vCam);
            }
        }
    }

    private void Start()
    {
        StageManager.RegisterStage(this);
        if (Index == 0)
        {
            StageManager.SetStage(0);
        }
    }

    public Vector3 GetSpawnPoint()
    {
        return _spawnPoint.transform.position;
    }
}
