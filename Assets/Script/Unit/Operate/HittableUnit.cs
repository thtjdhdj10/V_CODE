using UnityEngine;
using System.Collections.Generic;

public class HittableUnit : OperateUnit
{
    protected virtual void Awake()
    {
        hittableUnitList.Add(this);
    }

    public virtual Unit CollisionCheck()
    {
        for (int i = 0; i < OperateUnit.beHittableUnitList.Count; ++i)
        {
            Unit target = OperateUnit.beHittableUnitList[i].owner;

            if (CollisionCheck(target) == true)
            {
                return target;
            }
        }

        return null;
    }

    public virtual bool CollisionCheck(Unit target)
    {
        float distance = owner.logicalSize + target.logicalSize;

        return VEasyCalculator.IntersectCircle(owner.transform.position, target.transform.position, distance);
    }
}
