using UnityEngine;
using System.Collections.Generic;

public class PlayerBulletProjectile : ProjectileUnit {

    public string modelName;
    public float cooldown;
    public float damage;
    public float speed;
    public float distance;
    public float deltaAngle;

    protected override void Awake()
    {
        base.Awake();

        AddPlayerBullet(-deltaAngle * 2.5f);
        AddPlayerBullet(-deltaAngle * 1.5f);
        AddPlayerBullet(-deltaAngle * 0.5f);
        AddPlayerBullet(+deltaAngle * 0.5f);
        AddPlayerBullet(+deltaAngle * 1.5f);
        AddPlayerBullet(+deltaAngle * 2.5f);
    }
    
    public void AddPlayerBullet(float deltaDirection)
    {
        PlayerBullet db = new PlayerBullet();
        db.modelName = modelName;
        db.cooldown = cooldown;
        db.remainCooldown = cooldown;
        db.damage = damage;
        db.speed = speed;
        db.directionDelta = deltaDirection;

        fireBulletList.Add(db);
    }

    protected override bool FireBullet(int idx)
    {
        PlayerAttack pa = Player.player.GetComponent<PlayerAttack>();
        if (pa.attackTypeStateDic[PlayerAttack.AttackType.BULLET] == false)
        {
            return false;
        }

        PlayerBullet pb = (PlayerBullet)fireBulletList[idx];

        string targetName = pb.modelName;

        GameObject bullet = VEasyPoolerManager.GetObjectRequest(targetName);

        Unit unit = bullet.GetComponent<Unit>();

        if (unit == null)
        {
            Debug.LogError(targetName + " has no <Unit> component");
        }

        float direction = Player.player.transform.eulerAngles.z + pb.directionDelta + 90f;

        Vector3 deltaPos = VEasyCalculator.GetRotatedPosition(direction, distance);

        unit.transform.position = Player.player.transform.position + deltaPos;

        unit.movingUnit.InitStraightMove(pb.speed, direction);
        
        unit.hittableUnit.damage = pb.damage;

        return true;
    }
}
