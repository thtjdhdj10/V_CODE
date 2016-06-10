using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    //

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

    protected ControlableUnit.Direction CheckTerritory(Vector2 pos, float size)
    {
        Rect rect = CameraManager.manager.GetLogicalRect();

        if (pos.x - size < rect.xMin)
        {
            return ControlableUnit.Direction.LEFT;
        }

        if (pos.x + size > rect.xMax)
        {
            return ControlableUnit.Direction.RIGHT;
        }

        if (pos.y - size < rect.yMin)
        {
            return ControlableUnit.Direction.DOWN;
        }

        if (pos.y + size > rect.yMax)
        {
            return ControlableUnit.Direction.UP;
        }

        return ControlableUnit.Direction.NONE;
    }

}
