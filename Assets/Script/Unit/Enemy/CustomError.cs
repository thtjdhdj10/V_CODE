using UnityEngine;
using System.Collections;

public class CustomError : Error
{
    public Weapon weapon;
    public Body body;

    public float moveDirection;

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


    }

    void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        BodyFrame();

        WeaponFrame();
    }

    void Update()
    {

        UpdateSpriteColor();
    }

    //

    void BodyFrame()
    {
        switch (body)
        {
            case Body.BELL:
            case Body.POWER_BELL:
                {
                    BellFrame();
                }
                break;
            case Body.CIRCLE:
            case Body.POWER_CIRCLE:
                {
                    CircleFrame();
                }
                break;
            case Body.SQUARE:
            case Body.POWER_SQUARE:
                {
                    SquareFrame();
                }
                break;
            case Body.GLIDER:
                {
                    GliderFrame();
                }
                break;
        }
    }

    void BellFrame()
    {

    }

    void CircleFrame()
    {
        float moveDistance = currentAbilityDic[BasicAbility.MOVE_SPEED] * Time.fixedDeltaTime;

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(moveDirection, moveDistance);
        Vector2 v2Pos = transform.position;
        transform.position = v2Pos + moveVector;

        float dirToPlayer = VEasyCalculator.GetDirection(transform.position, Player.player.transform.position);

        if (weapon != Weapon.SPEAR)
        {
            Vector3 rot = transform.eulerAngles;
            rot.z = dirToPlayer + SpriteManager.spriteDefaultRotation;
            transform.eulerAngles = rot;
        }

        if (CheckTerritory(v2Pos, currentAbilityDic[BasicAbility.LOGICAL_SIZE]) != ControlableUnit.Direction.NONE)
        {
            moveDirection = dirToPlayer;
        }
    }

    void SquareFrame()
    {

    }

    void GliderFrame()
    {
        float moveDistance = currentAbilityDic[BasicAbility.MOVE_SPEED] * Time.fixedDeltaTime;
        
        float dirToPlayer = VEasyCalculator.GetDirection(transform.position, Player.player.transform.position);

        float disToPlayer = Vector2.Distance(Player.player.transform.position, transform.position);

        //moveDirection = VEasyCalculator.GetLerpDirection(
        //    moveDirection, dirToPlayer, currentAbilityDic[BasicAbility.TURN_AMOUNT] * Time.fixedDeltaTime,
        //    2f, 0.5f, disToPlayer, 0.2f);

        moveDirection = VEasyCalculator.GetTurningDirection(
            moveDirection, dirToPlayer, currentAbilityDic[BasicAbility.TURN_AMOUNT] * Time.fixedDeltaTime,
            2f, 0.5f, disToPlayer, 60f * Time.fixedDeltaTime);

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(moveDirection, moveDistance);

        Vector3 rot = transform.eulerAngles;
        rot.z = moveDirection + SpriteManager.spriteDefaultRotation;
        transform.eulerAngles = rot;

        Vector2 v2Pos = transform.position;
        transform.position = v2Pos + moveVector;
    }

    //

    void WeaponFrame()
    {
        switch(weapon)
        {
            case Weapon.BOLT:
                {
                    BoltFrame();
                }
                break;
            case Weapon.LAUNCHER:
                {
                    LauncherFrmae(); 
                }
                break;
            case Weapon.LASER:
                {
                    LaserFrame();
                }
                break;
            case Weapon.BAZOOKA:
                {
                    Bazooka();
                }
                break;
        }
    }

    void BoltFrame()
    {

    }

    void LauncherFrmae()
    {

    }

    void LaserFrame()
    {

    }

    void Bazooka()
    {

    }
}
