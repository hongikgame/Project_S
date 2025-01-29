using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetectorBase
{
    public float DetectionMargin = Physics2D.defaultContactOffset * 3;
    public abstract void UpdateDetector(DetectorData detectorData, DetectorStaticData staticData);
}
