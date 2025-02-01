using UnityEngine;

public static class Utils
{
    public static Vector3 GetPositionFromAngle(float rad, float angle)
    {
        Vector3 pos = Vector3.zero;

        angle = DegreeToRadian(angle);
        pos.x = Mathf.Cos(angle) * rad;
        pos.y = Mathf.Sin(angle) * rad;

        return pos;
    }

    public static float DegreeToRadian(float angle)
    {
        return Mathf.PI * angle / 180;
    }

    public static float GetAngle(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}
