using UnityEngine;
using System.Collections;

public class MicroBeetle : Virus {

    public enum FormType
    {
        NORMAL,
        SECOND_FORM,
    }

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    void FixedUpdate()
    {
        movingUnit.SetSpriteAngle();
    }

    public override void Init()
    {
        force = Force.ENEMY;

        type = Type.MICRO_BEETLE;

//        CreateUnit(1f);

        SpriteSetting();

        SetColorPerHealth();

        SetAbility(power, type);

        OperateComponentInit(true, true, true, false, false);
        if (projectileUnit == null)
            projectileUnit = gameObject.AddComponent<CyclePattern>();

        InitSprite();

        InitMovingModule();

        InitAttackModule();

        //colType = ColliderType.CIRCLE;
        //colCircle = 0.8f;

        colType = ColliderType.RECT;
        colRect.x = 0.4f;
        colRect.y = 0.7f;
    }

    public override void InitMovingModule()
    {
        float dirToPlayer = VEasyCalculator.GetDirection(transform.position, Player.player.transform.position);

        //movingUnit.InitRegularCurvePerDistanceMove(currentAbilityDic[BasicAbility.MOVE_SPEED],
        //    dirToPlayer,
        //    Player.player,
        //    currentAbilityDic[BasicAbility.TURN_AMOUNT],
        //    2f, 0.5f, 60f);

        movingUnit.InitLerpCurve(currentAbilityDic[BasicAbility.MOVE_SPEED],
            dirToPlayer,
            Player.player,
            currentAbilityDic[BasicAbility.TURN_AMOUNT],
            MovingUnit.BounceType.BOUNCE_WALL);
    }

    public override void InitAttackModule()
    {
        projectileUnit.activePatternList.Clear();

        ProjectileUnit.Scratch patternScratch = new ProjectileUnit.Scratch();
        patternScratch.cooldown = 8f;
        patternScratch.remainCooldown = patternScratch.cooldown;
        patternScratch.delay = 0.4f;
        patternScratch.count = 10;
        patternScratch.size = 1f;
        patternScratch.speed = 10f;
        patternScratch.modelName = "ScratchTimer";

        projectileUnit.activePatternList.Add(patternScratch);


    }

    public override void InitSprite()
    {
        for (int i = 0; i < spriteList.Count; ++i)
        {
            Color c = spriteList[i].color;
            c.a = 1f;
            spriteList[i].color = c;
        }
    }

}
