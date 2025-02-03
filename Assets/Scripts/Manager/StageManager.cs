using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StageManager
{
    private static List<Stage> _stageList = new List<Stage>();
    private static int _nextStageIndex = 0;

    public static int StageIndex { get => _nextStageIndex; }

    static StageManager()
    {
        SceneManager.activeSceneChanged += ResetStage;
    }

    private static void ResetStage(Scene previousScene, Scene newScene)
    {
        _stageList.Clear();
        _nextStageIndex = 0;
    }

    public static void RegisterStage(Stage stage)
    {
        _stageList.Add(stage);
        _stageList.Sort((s1, s2) =>  s1.Index.CompareTo(s2.Index));
        stage.gameObject.SetActive(false);
    }

    public static void SetStage(int index)
    {
        _nextStageIndex = index;

        

        InputManager.SetPlayerInputActivate(false);
        UI_HUD.Instance.FadeOutAndIn(TransitionType.Stage, 0.75f, OnFadeOutFinish, OnFadeInFinish);

        SaveLoadManager.Save();
    }

    private static void SetCameraConfiner(int index)
    {
        CinemachineBrain cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        if (cinemachineBrain == null || cinemachineBrain.ActiveVirtualCamera == null || _stageList.Count <= index) return;

        if (cinemachineBrain.ActiveVirtualCamera is CinemachineVirtualCamera vcam)
        {
            CinemachineConfiner2D confiner = vcam.GetComponent<CinemachineConfiner2D>();

            if (confiner != null)
            {
                confiner.m_BoundingShape2D = _stageList[index].CameraConfiner2D;
            }
        }
    }

    public static void OnFadeOutFinish()
    {
        //SetCameraConfiner(_nextStageIndex);

        if (_nextStageIndex - 1 >= 0)
        {
            _stageList[_nextStageIndex - 1].gameObject.SetActive(false);
        }

        _stageList[_nextStageIndex].gameObject.SetActive(true);
        CameraManager.Instance.RegisterVirtualCameras(_stageList[_nextStageIndex].VirtualCameraList);
        CharacterManager.MoveCharacterTo("Player", _stageList[_nextStageIndex].GetSpawnPoint());
    }

    public static void OnFadeInFinish()
    {
        InputManager.SetPlayerInputActivate(true);
    }


}
