using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CyclePattern : ProjectileUnit {

    public int lastActiveIndex;

    protected override void Awake()
    {
        base.Awake();

        lastActiveIndex = -1;
    }

    void OnEnable()
    {
        createDelay = 2f;
    }

    protected override void ActiveCheckFrame()
    {
        if (activePatternList.Count == 0)
            return;

        if (lastActiveIndex < activePatternList.Count &&
            lastActiveIndex >= 0)
        {
            if (activePatternList[lastActiveIndex].remainCooldown > 0f)
            {
                activePatternList[lastActiveIndex].remainCooldown -= Time.deltaTime;

                return;
            }
        }

        int activeIndex = lastActiveIndex + 1;
        if(activeIndex >= activePatternList.Count ||
            activeIndex < 0)
        {
            activeIndex = 0;
        }

        bool actived = ActivePattern(activeIndex);
        if(actived)
        {
            if (lastActiveIndex < activePatternList.Count &&
                lastActiveIndex >= 0)
            {
                activePatternList[lastActiveIndex].remainCooldown = activePatternList[lastActiveIndex].cooldown;
            }

            lastActiveIndex = activeIndex;
        }
    }

    protected override bool ActivePattern(int idx)
    {
        if(activePatternList[idx].GetType() == typeof(Scratch))
        {
            return ActiveScratch(idx);
        }

        return false;
    }

    bool ActiveScratch(int idx)
    {
        StartCoroutine(ActionScratch(activePatternList[idx] as Scratch));

        return true;
    }

    IEnumerator ActionScratch(Scratch factor)
    {
        float dirToPlayer = VEasyCalculator.GetDirection(owner, Player.player);

        owner.movingUnit.InitStraightMove(factor.speed, dirToPlayer, MovingUnit.BounceType.BOUNCE_WALL);

        owner.hittableUnit.enabled = false;

        for(int i = 0 ; i <owner.spriteList.Count;++i)
        {
            Color c = owner.spriteList[i].color;
            c.a = 0.1f;
            owner.spriteList[i].color = c;
        }

        //

        List<GameObject> scratchInitObjList =
            VEasyPoolerManager.GetObjectListRequest(factor.modelName, factor.count, false);

        for (int i = 0; i < scratchInitObjList.Count; ++i)
        {
            scratchInitObjList[i].GetComponent<ObjectState>().IsUse = true;

            scratchInitObjList[i].transform.position = owner.transform.position;

            scratchInitObjList[i].transform.eulerAngles = owner.transform.eulerAngles;

            yield return new WaitForSeconds(factor.delay);
        }

        owner.InitMovingModule();

        owner.InitSprite();

        owner.hittableUnit.enabled = true;

        yield break;
    }
}
