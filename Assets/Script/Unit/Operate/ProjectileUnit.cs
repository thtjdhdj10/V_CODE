using UnityEngine;
using System.Collections.Generic;

public class ProjectileUnit : OperateUnit
{
    public float createDelay;

    public class Bullet
    {
        public string modelName;
        public float cooldown;
        public float remainCooldown;
        public float speed;
    }

    public class PlayerBullet : Bullet
    {
        public float directionDelta;
        public float damage;
    }

    public class BazookaAttack : Bullet
    {
        public float analyzeTime;
        public float explosionRange;
        public float attackRange;
    }

    public List<Bullet> fireBulletList = new List<Bullet>();

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
            FireFrame();
        }
    }

    protected virtual void FireFrame()
    {
        for (int i = 0; i < fireBulletList.Count;++i )
        {
            fireBulletList[i].remainCooldown -= Time.deltaTime;

            if(fireBulletList[i].remainCooldown < 0f)
            {
                bool fired = FireBullet(i);

                if(fired == true)
                {
                    fireBulletList[i].remainCooldown = fireBulletList[i].cooldown;
                }
            }
        }
    }

    protected virtual bool FireBullet(int idx)
    {
        string targetName = fireBulletList[idx].modelName;

        GameObject obj = VEasyPoolerManager.GetObjectRequest(targetName);
        Unit unit = obj.GetComponent<Unit>();

        unit.OperateComponentInit(true, false, true, false, false);

        obj.transform.position = owner.transform.position;

//        float direction = owner.transform.eulerAngles.z - SpriteManager.spriteDefaultRotation;

        float direction = VEasyCalculator.GetDirection(owner, Player.player);

        unit.movingUnit.InitStraightMove(fireBulletList[idx].speed, direction);
     
        return true;
    }
}
