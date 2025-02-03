using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEditor;
using System.Linq;

public class CameraManager : SingletonMonobehavior<CameraManager>
{
    [SerializeField] private List<CinemachineVirtualCamera> _allVirtualCameras;

    [Header("Y Damping during player jump/fall ")]
    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;
    public float _fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping { get; private set; }
    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine _lerpYPanCoroutine;
    private Coroutine _panCameraCoroutine;

    private CinemachineBrain _cinemachineBrain;
    private CinemachineFramingTransposer _framingTransposer;
    private CinemachineVirtualCamera _currentCamera;

    private float _normYPanAmount;

    private Vector2 _startingTrackedObjectOffset;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        _cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void OnDisable()
    {
        _cinemachineBrain = null;

        _allVirtualCameras.Clear();
    }

    public void RegisterVirtualCameras(List<CinemachineVirtualCamera> cameras)
    {
        _allVirtualCameras = cameras;
        for (int i = 0; i < _allVirtualCameras.Count; i++)
        {
            if (_allVirtualCameras[i].enabled)
            {
                _currentCamera = _allVirtualCameras[i];

                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

        _normYPanAmount = _framingTransposer.m_YDamping;

        //设置被跟踪对象偏移的起始位置
        _startingTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
    }

    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        //获取初始的阻尼（damping）值
        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        //确定最终的阻尼(endDampAmount)值
        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = _normYPanAmount;
        }

        //对平移量（pan amount）进行插值处理
        float elapsedTime = 0f;
        while(elapsedTime < _fallYPanTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / _fallYPanTime));
            _framingTransposer.m_YDamping = lerpedPanAmount;
            yield return null;
        }
        IsLerpingYDamping = false;
    }

    public void PanCameraOnContact(float panDistance,float panTime,PanDirection panDirection,bool panToStartingPos)
    {
        _panCameraCoroutine = StartCoroutine(PanCamera(panDistance,panTime,panDirection,panToStartingPos));
    }

    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos = Vector2.zero;

        //处理触发器的平移
        if (!panToStartingPos)
        {
            //set direction and distance
            switch(panDirection)
            {
                case PanDirection.up:
                    endPos = Vector2.up;
                    break;
                case PanDirection.down:
                    endPos = Vector2.down;
                    break;
                case PanDirection.left:
                    endPos = Vector2.right;
                    break;
                case PanDirection.right:
                    endPos = Vector2.left;
                    break;
                default: 
                    break;
            }
            endPos *= panDistance;
            startingPos = _startingTrackedObjectOffset;
            endPos += startingPos;
        }
        //在移动回起始位置时，处理方向设置
        else
        {
            startingPos = _framingTransposer.m_TrackedObjectOffset;
            endPos = _startingTrackedObjectOffset;
        }
        //处理摄像机的实际平移
        float elapsedTime = 0f;
        while(elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;

            Vector3 panLerp = Vector3.Lerp(startingPos, endPos, (elapsedTime / panTime));
            _framingTransposer.m_TrackedObjectOffset = panLerp;
            yield return null;
        }
    }

    public void SwapCamera(CinemachineVirtualCamera cameraFromLeft, CinemachineVirtualCamera cameraFromRight, Vector2 triggerExitDirection)
    {
        // 如果当前相机是左侧相机，并且我们的触发器出口方向在右侧
        // if the current camera is the camera on the left and our trigger exit direction was on the right
        if (_currentCamera == cameraFromLeft && triggerExitDirection.x > 0f)
        {
            // activate the new camera
            cameraFromRight.enabled = true;

            // deactivate the old camera
            cameraFromLeft.enabled = false;

            // set the new camera as the current camera
            _currentCamera = cameraFromRight;

            // update our composer variable
            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        // 如果当前相机是右侧相机，并且我们的触发器命中方向在左侧
        // if the current camera is the camera on the right and our trigger hit direction was on the left
        else if (_currentCamera == cameraFromRight && triggerExitDirection.x < 0f)
        {
            // activate the new camera
            cameraFromLeft.enabled = true;

            // deactivate the old camera
            cameraFromRight.enabled = false;

            // set the new camera as the current camera
            _currentCamera = cameraFromLeft;

            // update our composer variable
            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }

    public void ChangeCameraSize(float targetSize, float duration = 1f)
    {
        var iCamera = _cinemachineBrain.ActiveVirtualCamera;
        if (iCamera != null && iCamera is CinemachineVirtualCamera vCamera)
        {
            float prevSize = vCamera.m_Lens.OrthographicSize;
            DOTween.To(() => vCamera.m_Lens.OrthographicSize, x => vCamera.m_Lens.OrthographicSize = x, targetSize, duration);

            var confiner = vCamera.GetComponent<CinemachineConfiner2D>();
            if (confiner != null)
            {
                confiner.InvalidateCache();
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CameraManager))]
    public class CameraManagerEditor : Editor
    {
        public float targetOrthoSize = 6f;
        public float tweenDuration = 1f;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Label("Editor Only", EditorStyles.boldLabel);
            targetOrthoSize = EditorGUILayout.FloatField("targetOrthoSize", targetOrthoSize);
            tweenDuration = EditorGUILayout.FloatField("tweenDuration", tweenDuration);
            if (GUILayout.Button("Change Camera Size")) 
            {
                CameraManager.Instance.ChangeCameraSize(targetOrthoSize, tweenDuration); 
            }
        }
    }

#endif
}
