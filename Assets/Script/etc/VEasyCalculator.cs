using UnityEngine;
using System.Collections;

public class VEasyCalculator {

    public static bool IntersectRect(Vector2 from, Vector2 to, float r)
    {
        if (r < 0f)
            return false;

        if(to.x > from.x + r ||
            to.x < from.x - r ||
            
            to.y > from.y + r ||
            to.y < from.y - r)
        {
            return false;
        }

        return true;
    }

    public static bool IntersectCircle(Vector2 from, Vector2 to, float r)
    {
        if (r < 0f)
            return false;

        float deltaDistanceSquare = Vector2.SqrMagnitude(to - from);
        
        if (deltaDistanceSquare > r*r)
        {
            return false;
        }

        return true;
    }

    public static Vector2 GetRotatedPosition(float degrees, float distance)
    {
        float x = distance * Mathf.Cos(Mathf.Deg2Rad * degrees);
        float y = distance * Mathf.Sin(Mathf.Deg2Rad * degrees);

        return new Vector2(x, y);
    }

    public static Vector2 EstimateCollidingMoveSpeed(Vector2 from, Vector2 targetPos, Vector2 targetPrevPos, float analyzingTime, float collisionTime)
    {
        Vector2 collidingMoveSpeedForImmovableTarget = (targetPos - from) / collisionTime;
        Vector2 targetMoveSpeed = (targetPos - targetPrevPos) / analyzingTime;

        return collidingMoveSpeedForImmovableTarget - targetMoveSpeed;
    }

    public static Vector2 EstimateCollidingPosition(Vector2 targetPos, Vector2 targetPrevPos, float analyzingTime, float collisionTime)
    {
        Vector2 targetMoveSpeed = (targetPos - targetPrevPos) / analyzingTime;

        return targetPos + (targetMoveSpeed * collisionTime);
    }
}
