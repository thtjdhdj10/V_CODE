using UnityEngine;
using System.Collections.Generic;

public class BeHittableUnit : OperateUnit
{
    protected virtual void Awake()
    {
        beHittableUnitList.Add(this);
    }
}
