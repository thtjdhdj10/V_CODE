using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public float logicalSize;

    protected HittableUnit hittableUnit;
    protected BeHittableUnit beHittableUnit;
    protected MovingUnit movingUnit;
    protected ControlableUnit controlableUnit;

    public enum Force
    {
        ENEMY,
        PLAYER,
        NONE,
    }

    public Force force = Force.ENEMY;

    //

    protected virtual void Awake()
    {
        hittableUnit = GetComponent<HittableUnit>();
        beHittableUnit = GetComponent<BeHittableUnit>();
        movingUnit = GetComponent<MovingUnit>();
        controlableUnit = GetComponent<ControlableUnit>();
    }

    public virtual void Init()
    {

    }

    protected bool CheckOutside(Vector2 pos, float size)
    {
        Rect rect = CameraManager.manager.GetLogicalRect();

        if(pos.x + size < rect.xMin||
            pos.x - size > rect.xMax ||
            pos.y + size < rect.yMin ||
            pos.y - size > rect.yMax)
        {
            return true;
        }

        return false;
    }

    protected PlayerMove.Direction CheckTerritory(Vector2 pos, float size)
    {
        Rect rect = CameraManager.manager.GetLogicalRect();

        if (pos.x - size < rect.xMin)
        {
            return PlayerMove.Direction.LEFT;
        }

        if (pos.x + size > rect.xMax)
        {
            return PlayerMove.Direction.RIGHT;
        }

        if (pos.y - size < rect.yMin)
        {
            return PlayerMove.Direction.DOWN;
        }

        if (pos.y + size > rect.yMax)
        {
            return PlayerMove.Direction.UP;
        }

        return PlayerMove.Direction.NONE;
    }

}
