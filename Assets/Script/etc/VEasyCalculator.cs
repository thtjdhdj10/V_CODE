﻿using UnityEngine;
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

        if (deltaDistanceSquare > r * r)
        {
            return false;
        }

        return true;
    }

    public static float GetDirection(Vector2 from, Vector2 to)
    {
        Vector2 v2 = (to - from).normalized;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }

    public static float GetTurningDirection(float from, float to, float degrees)
    {
        GetNormalizedDirection(ref from);
        GetNormalizedDirection(ref to);

        float delta = to - from;

        if (delta >= 180f)
            from += 360f;
        else if (delta < -180f)
            to += 360f;

        float completeDelta = to - from;

        if (completeDelta >= 0f)
        {
            return Mathf.Min(from + degrees, to);
        }
        else
        {
            return Mathf.Max(from - degrees, to);
        }
    }

    public static float GetTurningDirection(float from, float to, float degrees,
        float maxDistance, float minDistance, float currentDistance, float distanceFactor)
    {
        if (currentDistance <= minDistance)
            return GetTurningDirection(from, to, degrees);
        else if (currentDistance >= maxDistance)
            return GetTurningDirection(from, to, degrees + distanceFactor);

        float distanceRatio = (currentDistance - minDistance) / (maxDistance - minDistance);

        return GetTurningDirection(from, to, degrees + distanceFactor * distanceRatio);
    }

    public static float GetLerpDirection(float from, float to, float factor)
    {
        GetNormalizedDirection(ref from);
        GetNormalizedDirection(ref to);

        float delta = to - from;

        if (delta >= 180f)
            from += 360f;
        else if (delta < -180f)
            to += 360f;

        float completeDelta = to - from;
        float lerpedDegrees = to * factor + from * (1f - factor);

        if (completeDelta >= 0f)
        {
            return Mathf.Min(lerpedDegrees, to);
        }
        else
        {
            return Mathf.Max(lerpedDegrees, to);
        }
    }

    public static float GetLerpDirection(float from, float to, float factor,
        float maxDistance, float minDistance, float currentDistance, float distanceFactor)
    {
        if (currentDistance <= minDistance)
            return GetLerpDirection(from, to, factor);
        else if (currentDistance >= maxDistance)
            return GetLerpDirection(from, to, factor + distanceFactor);

        float distanceRatio = (currentDistance - minDistance) / (maxDistance - minDistance);

        return GetLerpDirection(from, to, factor + distanceFactor * distanceRatio);
    }

    public static void GetNormalizedDirection(ref float degrees)
    {
        if (degrees < 0f)
            degrees += 360f;
        else if (degrees >= 360f)
            degrees -= 360f;
    }

    public static Vector2 GetRotatedPosition(float degrees, float distance)
    {
        float x = distance * Mathf.Cos(Mathf.Deg2Rad * degrees);
        float y = distance * Mathf.Sin(Mathf.Deg2Rad * degrees);

        return new Vector2(x, y);
    }

    // analyzingTime 동안 targetPrevPos 에서 targetPos 로 이동한 적에게,
    // from 의 위치에서 "얼마"의 속도로 이동해야 collisionTime 에 충동하는지.
    public static Vector2 EstimateCollidingMoveSpeed(Vector2 from, Vector2 targetPos, Vector2 targetPrevPos, float analyzingTime, float collisionTime)
    {
        Vector2 collidingMoveSpeedForImmovableTarget = (targetPos - from) / collisionTime;
        Vector2 targetMoveSpeed = (targetPos - targetPrevPos) / analyzingTime;

        return collidingMoveSpeedForImmovableTarget - targetMoveSpeed;
    }

    // analyzingTime 동안 targetPrevPos 에서 targetPos 로 이동한 적이,
    // collisionTime 후에 도착하는 "위치".
    public static Vector2 EstimateCollidingPosition(Vector2 targetPos, Vector2 targetPrevPos, float analyzingTime, float collisionTime)
    {
        Vector2 targetMoveSpeed = (targetPos - targetPrevPos) / analyzingTime;

        return targetPos + (targetMoveSpeed * collisionTime);
    }
}
