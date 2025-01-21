using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBreath
{
    public float Oxygen { get; set; }
    public float MaxOxygen { get; }
    public bool CanSpendOxygen { get; }
    public float SpendOxygenInSecond { get; }

    public abstract void NoOxygen();
}
