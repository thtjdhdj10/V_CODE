using UnityEngine;
using System.Collections.Generic;

public class ProjectileUnit : OperateUnit
{
    public float createDelay;

    public class Pattern
    {
        public string modelName;
        public float cooldown;
        public float remainCooldown;
        public float speed;
    }

    public class PlayerBullet : Pattern
    {
        public float directionDelta;
        public float damage;
    }

    public class BazookaAttack : Pattern
    {
        public float analyzeTime;
        public float explosionRange;
        public float attackRange;
    }

    public class Scratch : Pattern
    {
        public int count;
        public float delay;
        public float size;
    }

    public List<Pattern> activePatternList = new List<Pattern>();

    void OnEnable()
    {
        createDelay = Random.Range(0f, 1f);
    }

    protected virtual void Update()
    {
        if (owner.unitActive == false)
            return;

        if (createDelay > 0f)
        {
            createDelay -= Time.deltaTime;
        }
        else
        {
            ActiveCheckFrame();
        }
    }

    protected virtual void ActiveCheckFrame()
    {
        for (int i = 0; i < activePatternList.Count; ++i)
        {
            activePatternList[i].remainCooldown -= Time.deltaTime;

            if (activePatternList[i].remainCooldown < 0f)
            {
                bool actived = ActivePattern(i);

                if (actived)
                {
                    activePatternList[i].remainCooldown = activePatternList[i].cooldown;
                }
            }
        }
    }

    protected virtual bool ActivePattern(int idx)
    {
        string targetName = activePatternList[idx].modelName;

        GameObject obj = VEasyPoolerManager.GetObjectRequest(targetName);
        Unit unit = obj.GetComponent<Unit>();

        unit.OperateComponentInit(true, false, true, false, false);

        obj.transform.position = owner.transform.position;

//        float direction = owner.transform.eulerAngles.z - SpriteManager.spriteDefaultRotation;

        float direction = VEasyCalculator.GetDirection(owner, Player.player);

        unit.movingUnit.InitStraightMove(activePatternList[idx].speed, direction, MovingUnit.BounceType.NONE);
     
        return true;
    }
}
