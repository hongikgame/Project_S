using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveLoadManager
{
    private static string _gamePath = "";
    private static string _logPath = "";
    private static SaveData _saveData;

    public static SaveData SaveData { get => _saveData; }

    static SaveLoadManager()
    {
        _gamePath = Application.persistentDataPath + "/GameData";
        _logPath = Application.persistentDataPath + "/LogData";

#if UNITY_EDITOR
        Application.logMessageReceived += SaveLog;
#endif
    }

    private static void SaveLog(string log, string stackTrace, LogType type)
    {
        using (StreamWriter sw = new StreamWriter(_logPath))
        {
            sw.WriteLine($"{System.DateTime.Now}: [{type}]: {log}");
            if(type == LogType.Exception)
            {
                sw.WriteLine(stackTrace);
            }
        }
    }

    public static void Save()
    {
        if(_saveData == null) _saveData = new SaveData();
        //Scene & Stage
        _saveData.LastSceneName = SceneManager.GetActiveScene().name;
        _saveData.LastStageIndex = StageManager.StageIndex;

        //Player Data
        CharacterBase characterBase = CharacterManager.GetCharacter("Player").Character;
        PlayerCharacterBase playerCharacter = null;
        if (characterBase != null)
        {
            playerCharacter = characterBase as PlayerCharacterBase;
        }

        if (playerCharacter != null)
        {
            _saveData.CurrentPlayerHealth = playerCharacter.Health;
            _saveData.CurrentPlayerOxygen = playerCharacter.Oxygen;
        }

        //GameSystem
        _saveData.LastSaveDateTime = DateTime.Now.ToString("g");
        _saveData.TotalPlayTime = _saveData.TotalPlayTimeSpan.ToString();

        //Statistics

        string toJsonData = JsonUtility.ToJson(_saveData, true);

        File.WriteAllText(_gamePath, toJsonData);
    }

    public static void Load()
    {
        if (File.Exists(_gamePath))
        {
            string fromJsonData = File.ReadAllText(_gamePath);
            _saveData = JsonUtility.FromJson<SaveData>(fromJsonData);

            //GameSystem
            _saveData.TotalPlayTimeSpan = TimeSpan.Parse(_saveData.TotalPlayTime);
        }
        else
        {
            _saveData = new SaveData();
        }
    }
}