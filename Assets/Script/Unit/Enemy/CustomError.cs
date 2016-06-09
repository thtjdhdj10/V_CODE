using UnityEngine;
using System.Collections;

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


    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        BodyFrame();

        WeaponFrame();

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
        
    }

    void SquareFrame()
    {

    }

    void GliderFrame()
    {

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
