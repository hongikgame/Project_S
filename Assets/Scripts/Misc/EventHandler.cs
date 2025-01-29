using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<float, float> PlayerGetDamage;
    public static event Action<bool> PlayerInHallucination;

    public static void CallPlayerGetDamage(float health, float maxHealth)
    {
        PlayerGetDamage?.Invoke(health, maxHealth);
    }

    public static void CallPlayerInHallucination(bool hallucination)
    {
        PlayerInHallucination?.Invoke(hallucination);
    }
}
