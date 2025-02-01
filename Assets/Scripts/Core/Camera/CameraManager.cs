using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;

    [Header("Y Damping during player jump/fall ")]
    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;
    public float _fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping { get; private set; }
    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine _lerpYPanCoroutine;
    private Coroutine _panCameraCoroutine;

    private CinemachineFramingTransposer _framingTransposer;
    private CinemachineVirtualCamera _currentCamera;

    private float _normYPanAmount;

    private Vector2 _startingTrackedObjectOffset;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        for(int i = 0; i < _allVirtualCameras.Length; i++)
        {
            if (_allVirtualCameras[i].enabled)
            {
                _currentCamera = _allVirtualCameras[i];

                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
        //��Y�� Y Damping����ֵ���óɺ� Inspector�����õ�ֵһ��
        _normYPanAmount = _framingTransposer.m_YDamping;

        //���ñ����ٶ���ƫ�Ƶ���ʼλ��
        _startingTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
    }

    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        //��ȡ��ʼ�����ᣨdamping��ֵ
        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        //ȷ�����յ�����(endDampAmount)ֵ
        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = _normYPanAmount;
        }

        //��ƽ������pan amount�����в�ֵ����
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

        //����������ƽ��
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
        //���ƶ�����ʼλ��ʱ������������
        else
        {
            startingPos = _framingTransposer.m_TrackedObjectOffset;
            endPos = _startingTrackedObjectOffset;
        }
        //�����������ʵ��ƽ��
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
        // �����ǰ��������������������ǵĴ��������ڷ������Ҳ�
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

        // �����ǰ������Ҳ�������������ǵĴ��������з��������
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

}
