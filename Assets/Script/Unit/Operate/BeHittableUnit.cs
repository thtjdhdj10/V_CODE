using UnityEngine;
using System.Collections.Generic;

public class BeHittableUnit : OperateUnit
{
    protected override void Awake()
    {
        base.Awake();

        beHittableUnitList.Add(this);
    }
}
