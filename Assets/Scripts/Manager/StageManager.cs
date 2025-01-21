using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StageManager
{
    private static Stage _currentStageConfiner;
    private static List<Stage> _stageList = new List<Stage>();
    private static List<StageChanger> _changerList = new List<StageChanger>();


    public static void SetStage(Stage stage)
    {
        InputManager.SetPlayerInputActivate(false);

        UI_HUD.Instance.FadeOutAndIn(TransitionType.Stage, 0.25f, OnFadeInFinish);

        _currentStageConfiner = stage;
    }

    private static void OnFadeInFinish()
    {
        Debug.Log("OnFadeInFinish");
        CharacterManager.MoveCharacterTo("player", _currentStageConfiner.GetSpawnPoint());
        InputManager.SetPlayerInputActivate(true);
    }


}
