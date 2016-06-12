using UnityEngine;
using System.Collections.Generic;

public class OperateUnit : MonoBehaviour {

    public Unit owner;

    public static List<ControlableUnit> controlableUnitList = new List<ControlableUnit>();
    public static List<HittableUnit> hittableUnitList = new List<HittableUnit>();
    public static List<BeHittableUnit> beHittableUnitList = new List<BeHittableUnit>();
    public static List<MovingUnit> movingUnitList = new List<MovingUnit>();

    protected bool ConfirmExistence()
    {
        if (owner == null ||
            owner.isActiveAndEnabled == false)
        {
            return false;
        }

        return true;
    }
}
