using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : SingletonMonobehavior<SaveLoadManager>
{
    private static string _logPath = "";

    protected override void Awake()
    {
        _logPath = Path.Combine(Application.persistentDataPath, "log.txt");
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        Application.logMessageReceived -= SaveLog;
        Application.logMessageReceived += SaveLog;
#endif
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        Application.logMessageReceived -= SaveLog;
#endif
    }

    private void SaveLog(string log, string stackTrace, LogType type)
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
}