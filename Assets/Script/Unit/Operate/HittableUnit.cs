using UnityEngine;
using System.Collections.Generic;

public class HittableUnit : OperateUnit
{
    public float damage;

    protected override void Awake()
    {
        base.Awake();

        hittableUnitList.Add(this);
    }

    protected virtual void FixedUpdate()
    {
        if (owner.unitActive == false)
            return;

        Unit colUnit = CollisionCheck();
        if (colUnit == null)
            return;

        if (colUnit.unitActive == false)
            return;

        owner.Hit(colUnit);

        if(colUnit.force == Unit.Force.ENEMY)
        {
            Enemy target = colUnit as Enemy;

            if (target.unitActive == false)
                return;

            target.currentAbilityDic[Enemy.BasicAbility.HEALTH_POINT] -= damage;

            if (target.currentAbilityDic[Enemy.BasicAbility.HEALTH_POINT] < 0f)
            {
                target.ReleaseUnit(1f);
            }

//            Debug.Log(target.currentAbilityDic[Enemy.BasicAbility.HEALTH_POINT]);
        }
        else if(colUnit.force == Unit.Force.PLAYER)
        {
            Player.player.BeHit(owner);
        }
    }

    public virtual Unit CollisionCheck()
    {
        for (int i = 0; i < OperateUnit.beHittableUnitList.Count; ++i)
        {
            Unit target = OperateUnit.beHittableUnitList[i].owner;

            if (target == null)
                continue;

            if (target.gameObject.activeInHierarchy == false)
                continue;

            if (CollisionCheck(target) == true)
            {
                return target;
            }
        }

        return null;
    }

    public virtual bool CollisionCheck(Unit target)
    {
        if (target.force == owner.force)
            return false;

        float distance = owner.logicalSize + target.logicalSize;

        return VEasyCalculator.IntersectCircle(owner.transform.position, target.transform.position, distance);
    }
}
