using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DetectorData
{
    public bool IsGround;
    public bool IsOnWater;

    public DetectorData DeepCopy => this.MemberwiseClone() as DetectorData;
}
