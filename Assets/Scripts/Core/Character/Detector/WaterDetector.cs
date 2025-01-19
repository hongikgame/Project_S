using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDetector : DetectorBase
{
    private LayerMask _waterLayermask = LayerMask.GetMask("Water");

    public override void UpdateDetector(DetectorData detectorData, DetectorStaticData staticData)
    {
        Bounds bounds = staticData.Bounds;
        Vector2 origin = new Vector2(bounds.center.x, bounds.center.y);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, DetectionMargin, _waterLayermask);
        Debug.DrawRay(origin, Vector2.down * DetectionMargin, Color.red);

        detectorData.IsOnWater = hit.collider != null;
    }
}
