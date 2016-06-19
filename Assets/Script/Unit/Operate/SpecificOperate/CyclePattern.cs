﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CyclePattern : ProjectileUnit {

    public int lastActiveIndex;

    protected override void Awake()
    {
        base.Awake();

        lastActiveIndex = 0;
    }

    void OnEnable()
    {
        createDelay = 2f;
    }

    protected override void ActiveCheckFrame()
    {
        if(activePatternList[lastActiveIndex].remainCooldown > 0f)
        {
            activePatternList[lastActiveIndex].remainCooldown -= Time.deltaTime;
        }
        else
        {
            bool actived = ActivePattern(lastActiveIndex + 1);
            if(actived)
            {
                activePatternList[lastActiveIndex].remainCooldown = activePatternList[lastActiveIndex].cooldown;

                ++lastActiveIndex;
            }
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
        List<GameObject> scratchInitObjList =
            VEasyPoolerManager.GetObjectListRequest("ScratchTimer", factor.count);

        // TODO 

        yield break;
    }
}