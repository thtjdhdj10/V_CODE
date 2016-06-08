using UnityEngine;
using System.Collections;

public class CustomError : Error
{
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

    public Weapon weapon;
    public Body body;

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
        AbilityInit();

        SetColorPerHealth();


    }

    void Start()
    {
        Init();
    }

    void Update()
    {


        UpdateSpriteColor();
    }
}
