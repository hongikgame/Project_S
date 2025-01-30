using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonobehavior<GameManager>
{
    private bool _hallucination = false;

    public bool Hallucination
    {
        get { return _hallucination; }
        set
        {
            _hallucination = value;
            EventHandler.CallPlayerInHallucination(_hallucination);
        }
    }

    #region Monobehaviour

    protected override void Awake()
    {
        base.Awake();

        SaveLoadManager.Load();
    }

    private void Update()
    {
        UpdateSaveData();
    }

    #endregion

    private void UpdateSaveData()
    {
        if (Time.timeScale != 0)
        {
            SaveLoadManager.SaveData.TotalPlayTime += TimeSpan.FromSeconds(Time.deltaTime);
        }
    }

    private void RespawnPlayer(Scene scene, LoadSceneMode mode)
    {
        RespawnPlayer();
    }

    public void RespawnPlayer()
    {
        SceneManager.sceneLoaded -= RespawnPlayer;

        PlayerCharacterBase player = CharacterManager.GetCharacter("Player").Character as PlayerCharacterBase;
        if (player == null) return;

        player.RecoverFullHealth();

        StageManager.SetStage(SaveLoadManager.SaveData.LastStageIndex);
    }
}
