using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : DetectorBase
{
    private LayerMask _groundLayermask = LayerMask.GetMask("Ground");
    public override void UpdateDetector(DetectorData detectorData, DetectorStaticData staticData)
    {
        Bounds bounds = staticData.Bounds;
        Vector2 origin = new Vector2(bounds.center.x, bounds.center.y - bounds.size.y / 2);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, DetectionMargin, _groundLayermask);
        Debug.DrawRay(origin, Vector2.down * DetectionMargin, Color.red);

        detectorData.IsGround = hit.collider != null;
    }
}
