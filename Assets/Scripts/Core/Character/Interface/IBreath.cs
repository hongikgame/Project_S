using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBreath
{
    public float Oxygen { get; }
    public float MaxOxygen { get; }
    public bool CanSpendOxygen { get; }

    public void DecreaseOxygen(float amount);
    public void IncreaseOxygen(float amount);
}
