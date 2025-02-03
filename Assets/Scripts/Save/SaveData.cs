using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    //Scene & Stage
    public string LastSceneName;
    public int LastStageIndex;

    //Player Data
    public float CurrentPlayerHealth;
    public float CurrentPlayerOxygen;

    //GameSystem
    public TimeSpan TotalPlayTimeSpan;
    public string TotalPlayTime;
    public string LastSaveDateTime;

    //Statistics
    public int TotalKills;
    public int TotalDeads;
}
