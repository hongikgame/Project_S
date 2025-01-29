using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOrthiSizeChanger : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    public float targetSize = 4f; // 目标的正交投影大小 (목표의 Ortho Size 크기)
    public float duration = 2f; // 过渡持续时间 (총 과도시간)

    private float initialSize; // 初始的正交投影大小 (본래의 Camera 의 Ortho Size)
    private float processTime; // 记录过渡过程中经过的时间 (과도중의 겪은시간)
    public bool isTransition = false; 
    public bool isReturn = false;

    void Start()
    {
        if (_virtualCamera == null)
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
        initialSize = _virtualCamera.m_Lens.OrthographicSize;
        processTime = 0f;
    }

    void Update()
    {
        if (isTransition || isReturn)
        {
            // 计算过渡进度 (과도의진도를 계산)
            processTime += Time.deltaTime;
            float process = processTime / duration;

            if (isTransition)
            {
                _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(initialSize, targetSize, process);
            }
            else
            {
                _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(targetSize, initialSize, process);
            }
            // 如果过渡完成，则停止过渡 (과도가 완료되면 과도Stop)
            if (process >= 1f)
            {
                processTime = 0f;
                isTransition = false;
                isReturn = false;
            }
        }
    }
}
