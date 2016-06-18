using UnityEngine;
using System.Collections;

public class LevelHoldOut : LevelMaker
{
    public float clearTime = 20f;
    public float passedTime;

    void Update()
    {
        if(passedTime < clearTime)
        {
            passedTime += Time.deltaTime;
        }

        clearDegree = passedTime / clearTime;
    }
}
