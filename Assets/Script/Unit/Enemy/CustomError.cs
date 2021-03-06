﻿using UnityEngine;
using System.Collections.Generic;

public class CustomError : Error
{
    public Weapon weapon;
    public Body body;

    public enum Weapon
    {
        NONE,
        SPEAR,
        BOLT,
        BAZOOKA,
        LASER,
        LAUNCHER,
    }

    public enum Body
    {
        NONE,
        SQUARE,
        CIRCLE,
        BELL,
        GLIDER,
    }

    protected override void SetColorPerHealth()
    {
        healthColorDic[Health.DEAD] = Color.white;
        healthColorDic[Health.QUARTER] = Color.white;
        healthColorDic[Health.HALF] = new Color(1f, 1f, 0f);
        healthColorDic[Health.THREE_QUARTER] = new Color(1f, 0.4f, 0f);
        healthColorDic[Health.FULL] = new Color(1f, 0f, 0f);
    }

    public override void Init()
    {
        force = Force.ENEMY;

        CreateUnit(1f);

        SpriteSetting();

        SetAbility(power, body, weapon);

        OperateComponentInit(true, true, false, false, false);

        InitSprite();

        InitMovingModule();

        InitAttackModule();

        colType = ColliderType.CIRCLE;
        colCircle = 0.1f;
    }

    public override void InitMovingModule()
    {
        if (bodyTypeInitDic.ContainsKey(body) == true)
            bodyTypeInitDic[body]();
    }

    public override void InitAttackModule()
    {
        if (weaponTypeInitDic.ContainsKey(weapon) == true)
            weaponTypeInitDic[weapon]();
    }

    protected override void Awake()
    {
        base.Awake();

        FuncDicMatch();
    }

    void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        if (bodyTypeFrameDic.ContainsKey(body) == true)
            bodyTypeFrameDic[body]();

        if (weaponTypeFrameDic.ContainsKey(weapon) == true)
            weaponTypeFrameDic[weapon]();
    }

    void Update()
    {


        UpdateSpriteColor();
    }

    //

    delegate void CustomInit();

    Dictionary<Body, CustomInit> bodyTypeInitDic = new Dictionary<Body, CustomInit>();

    void CircleInit()
    {
        if (movingUnit == null)
            movingUnit = gameObject.AddComponent<MovingUnit>();

        float dirToPlayer = VEasyCalculator.GetDirection(transform.position, Player.player.transform.position);

        movingUnit.InitStraightMove(
            currentAbilityDic[BasicAbility.MOVE_SPEED],
            dirToPlayer, MovingUnit.BounceType.BOUNCE_WALL);
    }

    void GliderInit()
    {
        if (movingUnit == null)
            movingUnit = gameObject.AddComponent<MovingUnit>();

        float dirToPlayer = VEasyCalculator.GetDirection(transform.position, Player.player.transform.position);

        movingUnit.InitRegularCurvePerDistanceMove(currentAbilityDic[BasicAbility.MOVE_SPEED], dirToPlayer,
            Player.player, currentAbilityDic[BasicAbility.TURN_AMOUNT], 2f, 0.5f, 60f);
    }

    Dictionary<Weapon, CustomInit> weaponTypeInitDic = new Dictionary<Weapon, CustomInit>();

    void BoltInit()
    {
        if (projectileUnit == null)
            projectileUnit = gameObject.AddComponent<ProjectileUnit>();

        ProjectileUnit.Pattern bullet = new ProjectileUnit.Pattern();

        bullet.modelName = "Bolt";
        bullet.speed = 3f + 0.5f * power;
        bullet.cooldown = 4f / (2f + power * power);
        bullet.remainCooldown = bullet.cooldown;

        projectileUnit.activePatternList.Add(bullet);
    }

    void LaserInit()
    {
        if (projectileUnit == null)
            projectileUnit = gameObject.AddComponent<ProjectileUnit>();


    }

    void BazookaInit()
    {
        if (projectileUnit == null)
            projectileUnit = gameObject.AddComponent<BazookaProjectile>();

        ProjectileUnit.BazookaAttack bullet = new ProjectileUnit.BazookaAttack();

        bullet.modelName = "ExplosionTimer";
        bullet.analyzeTime = 1f;
        bullet.cooldown = 3f;
        bullet.remainCooldown = bullet.cooldown;
        bullet.explosionRange = 1f;

        projectileUnit.activePatternList.Add(bullet);
        //        bullet.attackRange


    }

    void LauncherInit()
    {
        if (projectileUnit == null)
            projectileUnit = gameObject.AddComponent<ProjectileUnit>();
    }

    //

    void FuncDicMatch()
    {
        bodyTypeInitDic[Body.CIRCLE] = CircleInit;
        bodyTypeInitDic[Body.GLIDER] = GliderInit;

        weaponTypeInitDic[Weapon.BOLT] = BoltInit;
        weaponTypeInitDic[Weapon.LASER] = LaserInit;
        weaponTypeInitDic[Weapon.LAUNCHER] = LauncherInit;
        weaponTypeInitDic[Weapon.BAZOOKA] = BazookaInit;

        bodyTypeFrameDic[Body.CIRCLE] = CircleFrame;
        bodyTypeFrameDic[Body.GLIDER] = GliderFrame;
        bodyTypeFrameDic[Body.BELL] = BellFrame;

        weaponTypeFrameDic[Weapon.BAZOOKA] = BazookaFrame;
        weaponTypeFrameDic[Weapon.BOLT] = BoltFrame;
        weaponTypeFrameDic[Weapon.LASER] = LaserFrame;
        weaponTypeFrameDic[Weapon.LAUNCHER] = LauncherFrame;
    }

    delegate void Frame();

    Dictionary<Body, Frame> bodyTypeFrameDic = new Dictionary<Body, Frame>();

    void CircleFrame()
    {
        float dirToPlayer = VEasyCalculator.GetDirection(this, Player.player);

        if (weapon != Weapon.SPEAR)
        {
            Vector3 rot = transform.eulerAngles;
            rot.z = dirToPlayer + SpriteManager.spriteDefaultRotation;
            transform.eulerAngles = rot;
        }
        else
        {
            movingUnit.SetSpriteAngle();
        }
    }

    void GliderFrame()
    {
        movingUnit.SetSpriteAngle();
    }

    void BellFrame()
    {
        if(weapon != Weapon.BAZOOKA &&
            weapon != Weapon.LASER)
        {
            float dirToPlayer = VEasyCalculator.GetDirection(this, Player.player);

            Vector3 rot = transform.eulerAngles;
            rot.z = dirToPlayer + SpriteManager.spriteDefaultRotation;
            transform.eulerAngles = rot;
        }
    }

    //

    Dictionary<Weapon, Frame> weaponTypeFrameDic = new Dictionary<Weapon, Frame>();

    void BoltFrame()
    {

    }

    void LauncherFrame()
    {

    }

    void LaserFrame()
    {

    }

    float bazookaAttackDirection;
    float bazookaAttackDistance;
    void BazookaFrame()
    {

    }
}
