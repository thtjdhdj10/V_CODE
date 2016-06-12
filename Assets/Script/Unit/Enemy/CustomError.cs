using UnityEngine;
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
        POWER_SQUARE,
        POWER_CIRCLE,
        POWER_BELL,
    }

    protected override void SetColorPerHealth()
    {
        healthColorDic[Health.DEAD] = Color.white;
        healthColorDic[Health.QUARTER] = Color.white;
        healthColorDic[Health.HALF] = new Color(1f, 1f, 0f);
        healthColorDic[Health.THREE_QUARTER] = new Color(1f, 0.75f, 0f);
        healthColorDic[Health.FULL] = new Color(1f, 0f, 0f);
    }

    public override void Init()
    {
        SetAbillity(power, body, weapon);

        SetColorPerHealth();

        if (BodyTypeOperateDic.ContainsKey(body) == true)
            BodyTypeOperateDic[body]();

        logicalSize = 0.12f;
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
        if (BodyTypeFrameDic.ContainsKey(body) == true)
            BodyTypeFrameDic[body]();

        if (WeaponTypeFrameDic.ContainsKey(weapon) == true)
            WeaponTypeFrameDic[weapon]();
    }

    void Update()
    {


        UpdateSpriteColor();
    }

    //

    delegate void OperateInit();

    Dictionary<Body, OperateInit> BodyTypeOperateDic = new Dictionary<Body, OperateInit>();

    void CircleInit()
    {
        if (movingUnit == null)
            return;

        movingUnit.target = Player.player;
        movingUnit.moveType = MovingUnit.MoveType.STRAIGHT;
        
        float dirToPlayer = VEasyCalculator.GetDirection(transform.position, Player.player.transform.position);
        movingUnit.moveDirection = dirToPlayer;
        movingUnit.moveSpeed = currentAbilityDic[BasicAbility.MOVE_SPEED];
    }

    void GliderInit()
    {
        if (movingUnit == null)
            return;

        movingUnit.target = Player.player;
        movingUnit.moveType = MovingUnit.MoveType.REGULAR_CURVE_PER_DISTANCE;

        float dirToPlayer = VEasyCalculator.GetDirection(transform.position, Player.player.transform.position);
        movingUnit.moveDirection = dirToPlayer;
        movingUnit.moveSpeed = currentAbilityDic[BasicAbility.MOVE_SPEED];
        movingUnit.curveFactor = currentAbilityDic[BasicAbility.TURN_AMOUNT];
        movingUnit.maxCurveDistance = 2f;
        movingUnit.minCurveDistance = 0.5f;
        movingUnit.distanceFactor = 60f;
    }

    //

    void FuncDicMatch()
    {
        BodyTypeOperateDic[Body.CIRCLE] = CircleInit;
        BodyTypeOperateDic[Body.POWER_CIRCLE] = CircleInit;
        BodyTypeOperateDic[Body.GLIDER] = GliderInit;

        BodyTypeFrameDic[Body.CIRCLE] = CircleFrame;
        BodyTypeFrameDic[Body.POWER_CIRCLE] = CircleFrame;
        BodyTypeFrameDic[Body.GLIDER] = GliderFrame;

        WeaponTypeFrameDic[Weapon.BAZOOKA] = BazookaFrame;
        WeaponTypeFrameDic[Weapon.BOLT] = BoltFrame;
        WeaponTypeFrameDic[Weapon.LASER] = LaserFrame;
        WeaponTypeFrameDic[Weapon.LAUNCHER] = LauncherFrame;
    }

    delegate void Frame();

    Dictionary<Body, Frame> BodyTypeFrameDic = new Dictionary<Body, Frame>();

    void CircleFrame()
    {
        float dirToPlayer = VEasyCalculator.GetDirection(transform.position, Player.player.transform.position);

        if (weapon != Weapon.SPEAR)
        {
            Vector3 rot = transform.eulerAngles;
            rot.z = dirToPlayer + SpriteManager.spriteDefaultRotation;
            transform.eulerAngles = rot;
        }

        if (CheckTerritory(transform.position, logicalSize) != PlayerMove.Direction.NONE)
        {
            movingUnit.moveDirection = dirToPlayer;
        }
    }

    void GliderFrame()
    {
        Vector3 rot = transform.eulerAngles;
        rot.z = movingUnit.moveDirection + SpriteManager.spriteDefaultRotation;
        transform.eulerAngles = rot;
    }

    //

    Dictionary<Weapon, Frame> WeaponTypeFrameDic = new Dictionary<Weapon, Frame>();

    void BoltFrame()
    {

    }

    void LauncherFrame()
    {

    }

    void LaserFrame()
    {

    }

    void BazookaFrame()
    {

    }
}
