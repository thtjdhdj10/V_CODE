using UnityEngine;
using System.Collections.Generic;

public class AbilityManager {

    public static void GetAbility(
        Dictionary<Enemy.BasicAbility,float> abilityDic,
        Dictionary<Enemy.BasicAbility,float> abilityFactorDic,
        CustomError.Body body, CustomError.Weapon weapon)
    {
        abilityDic.Clear();
        abilityFactorDic.Clear();

        switch(body)
        {
            case CustomError.Body.BELL:
            case CustomError.Body.POWER_BELL:
                {
                    abilityDic[Enemy.BasicAbility.HEALTH_POINT] = 60f;
                    abilityDic[Enemy.BasicAbility.IMPORTANCE] = 0f;
                    abilityDic[Enemy.BasicAbility.LOGICAL_SIZE] = 0.12f;
                    abilityDic[Enemy.BasicAbility.MOVE_SPEED] = 0f;
                    abilityDic[Enemy.BasicAbility.TURN_AMOUNT] = 1f;
                    
                    abilityFactorDic[Enemy.BasicAbility.HEALTH_POINT] = 30f;
                }
                break;
            case CustomError.Body.CIRCLE:
            case CustomError.Body.POWER_CIRCLE:
                {
                    abilityDic[Enemy.BasicAbility.HEALTH_POINT] = 40f;
                    abilityDic[Enemy.BasicAbility.LOGICAL_SIZE] = 0.12f;
                    abilityDic[Enemy.BasicAbility.MOVE_SPEED] = 1.5f;
                    abilityDic[Enemy.BasicAbility.TURN_AMOUNT] = 1f;
                    if (weapon == CustomError.Weapon.SPEAR)
                        abilityDic[Enemy.BasicAbility.TURN_AMOUNT] = 0f;
                    
                    abilityFactorDic[Enemy.BasicAbility.HEALTH_POINT] = 20f;
                    abilityFactorDic[Enemy.BasicAbility.MOVE_SPEED] = 0.35f;
                }
                break;
            case CustomError.Body.SQUARE:
            case CustomError.Body.POWER_SQUARE:
                {
                    abilityDic[Enemy.BasicAbility.HEALTH_POINT] = 80f;
                    abilityDic[Enemy.BasicAbility.IMPORTANCE] = 0f;
                    abilityDic[Enemy.BasicAbility.LOGICAL_SIZE] = 0.12f;
                    abilityDic[Enemy.BasicAbility.MOVE_SPEED] = 0f;
                    abilityDic[Enemy.BasicAbility.TURN_AMOUNT] = 0f;

                    abilityFactorDic[Enemy.BasicAbility.HEALTH_POINT] = 40f;
                }
                break;
            case CustomError.Body.GLIDER:
                {
                    abilityDic[Enemy.BasicAbility.HEALTH_POINT] = 20f;
                    abilityDic[Enemy.BasicAbility.LOGICAL_SIZE] = 0.12f;
                    abilityDic[Enemy.BasicAbility.MOVE_SPEED] = 2.5f;
                    abilityDic[Enemy.BasicAbility.TURN_AMOUNT] = 0.1f;

                    abilityFactorDic[Enemy.BasicAbility.HEALTH_POINT] = 20f;
                    abilityFactorDic[Enemy.BasicAbility.MOVE_SPEED] = 0.5f;
                    abilityFactorDic[Enemy.BasicAbility.TURN_AMOUNT] = 0.015f;
                }
                break;
            default:
                {
                    CustomLog.CompleteLogError(body + " is invalid Body Type");
                }
                break;
        }
    }

    public static void GetAbility(
        Dictionary<Enemy.BasicAbility, float> abilityDic,
        Dictionary<Enemy.BasicAbility, float> abilityFactorDic,
        Virus.Type type)
    {
        abilityDic.Clear();
        abilityFactorDic.Clear();


    }

    public static void GetAbility(
        Dictionary<Enemy.BasicAbility, float> abilityDic,
        Dictionary<Enemy.BasicAbility, float> abilityFactorDic,
        SpecialError.Type type)
    {
        abilityDic.Clear();
        abilityFactorDic.Clear();


    }

    public static void GetAttackAbility(
        List<Dictionary<Enemy.AttackAbility, float>> abilityDicList,
        List<Dictionary<Enemy.AttackAbility, float>> abilityFactorDicList,
        CustomError.Body body, CustomError.Weapon weapon)
    {
        abilityDicList.Clear();
        abilityFactorDicList.Clear();

        abilityDicList.Add(new Dictionary<Enemy.AttackAbility, float>());
        abilityFactorDicList.Add(new Dictionary<Enemy.AttackAbility, float>());

        GetAttackAbility(abilityDicList[0], abilityFactorDicList[0], body, weapon);
    }

    public static void GetAttackAbility(
        Dictionary<Enemy.AttackAbility, float> abilityDic,
        Dictionary<Enemy.AttackAbility, float> abilityFactorDic,
        CustomError.Body body, CustomError.Weapon weapon)
    {
        abilityDic.Clear();
        abilityFactorDic.Clear();

        switch(weapon)
        {
            case CustomError.Weapon.NONE:
                {

                }
                break;
            case CustomError.Weapon.SPEAR:
                {

                }
                break;
            case CustomError.Weapon.BOLT:
                {
                    abilityDic[Enemy.AttackAbility.ATTACK_DELAY] = 2f;
                    abilityDic[Enemy.AttackAbility.MIN_ATTACK_RANGE] = 0.75f;
                    abilityDic[Enemy.AttackAbility.PROJECTILE_SPEED] = 3f;

                    abilityFactorDic[Enemy.AttackAbility.ATTACK_DELAY] = 0.25f;
                    abilityFactorDic[Enemy.AttackAbility.MIN_ATTACK_RANGE] = 0.1f;
                    abilityFactorDic[Enemy.AttackAbility.PROJECTILE_SPEED] = 0.5f;

                    if(body == CustomError.Body.POWER_BELL ||
                        body == CustomError.Body.POWER_CIRCLE ||
                        body == CustomError.Body.POWER_SQUARE)
                    {
                        abilityDic[Enemy.AttackAbility.ATTACK_DELAY] = 1f;
                    }
                }
                break;
            case CustomError.Weapon.LASER:
                {
                    abilityDic[Enemy.AttackAbility.ATTACK_DELAY] = 4.5f;
                    abilityDic[Enemy.AttackAbility.MAX_ATTACK_RANGE] = 5f;
                    abilityDic[Enemy.AttackAbility.EXPLOSION_RANGE] = 1f;

                    abilityFactorDic[Enemy.AttackAbility.ATTACK_DELAY] = 0.25f;
                    abilityFactorDic[Enemy.AttackAbility.MAX_ATTACK_RANGE] = 1f;
                    abilityFactorDic[Enemy.AttackAbility.EXPLOSION_RANGE] = 0.125f;

                    if (body == CustomError.Body.POWER_BELL ||
                        body == CustomError.Body.POWER_CIRCLE ||
                        body == CustomError.Body.POWER_SQUARE)
                    {
                        abilityDic[Enemy.AttackAbility.MAX_ATTACK_RANGE] = 15f;
                        abilityDic[Enemy.AttackAbility.EXPLOSION_RANGE] = 1.5f;

                        abilityFactorDic[Enemy.AttackAbility.EXPLOSION_RANGE] = 0.125f;
                        abilityFactorDic[Enemy.AttackAbility.MAX_ATTACK_RANGE] = 0f;
                    }
                }
                break;
            case CustomError.Weapon.LAUNCHER:
                {
                    abilityDic[Enemy.AttackAbility.ATTACK_DELAY] = 5f;
                    abilityFactorDic[Enemy.AttackAbility.ATTACK_DELAY] = 0.4f;
                    abilityDic[Enemy.AttackAbility.MIN_ATTACK_RANGE] = 0.5f;
                    abilityDic[Enemy.AttackAbility.PROJECTILE_SPEED] = 1f;
                    abilityDic[Enemy.AttackAbility.ACCELERATION_PER_SEC] = 0.3f;
                    abilityFactorDic[Enemy.AttackAbility.ACCELERATION_PER_SEC] = 0.15f;
                    abilityDic[Enemy.AttackAbility.TRIGGER_DELAY] = 2.5f;
                    abilityFactorDic[Enemy.AttackAbility.TRIGGER_DELAY] = -0.25f;
                    abilityDic[Enemy.AttackAbility.EXPLOSION_RANGE] = 1f;
                    abilityFactorDic[Enemy.AttackAbility.EXPLOSION_RANGE] = 0.25f;
                    abilityDic[Enemy.AttackAbility.TURN_AMOUNT] = 0.1f;
                    abilityFactorDic[Enemy.AttackAbility.TURN_AMOUNT] = 0.035f;

                    if (body == CustomError.Body.POWER_BELL ||
                        body == CustomError.Body.POWER_CIRCLE ||
                        body == CustomError.Body.POWER_SQUARE)
                    {
                        abilityDic[Enemy.AttackAbility.ATTACK_DELAY] = 3f;
                        abilityDic[Enemy.AttackAbility.ACCELERATION_PER_SEC] = 0.6f;
                        abilityDic[Enemy.AttackAbility.TRIGGER_DELAY] = 1.5f;
                        abilityFactorDic[Enemy.AttackAbility.TRIGGER_DELAY] = 0f;
                        abilityDic[Enemy.AttackAbility.EXPLOSION_RANGE] = 1.5f;
                        abilityDic[Enemy.AttackAbility.TURN_AMOUNT] = 0.2f;
                    }
                }
                break;
            case CustomError.Weapon.BAZOOKA:
                {
                    abilityDic[Enemy.AttackAbility.ATTACK_DELAY] = 5f;
                    abilityFactorDic[Enemy.AttackAbility.ATTACK_DELAY] = 0.25f;
                    abilityDic[Enemy.AttackAbility.MAX_ATTACK_RANGE] = 1.6f;
                    abilityFactorDic[Enemy.AttackAbility.MAX_ATTACK_RANGE] = 0.4f;
                    abilityDic[Enemy.AttackAbility.EXPLOSION_RANGE] = 1f;
                    abilityFactorDic[Enemy.AttackAbility.EXPLOSION_RANGE] = 0.25f;

                    if (body == CustomError.Body.POWER_BELL ||
                        body == CustomError.Body.POWER_CIRCLE ||
                        body == CustomError.Body.POWER_SQUARE)
                    {
                        abilityDic[Enemy.AttackAbility.ATTACK_DELAY] = 3f;
                        abilityDic[Enemy.AttackAbility.MAX_ATTACK_RANGE] = 3f;
                        abilityDic[Enemy.AttackAbility.EXPLOSION_RANGE] = 1.5f;
                    }
                }
                break;
            default:
                {
                    CustomLog.CompleteLogError(weapon + " is invalid Weapon Type");
                }
                break;
        }
    }

    public static void GetAttackAbility(
        List<Dictionary<Enemy.AttackAbility, float>> abilityDicList,
        List<Dictionary<Enemy.AttackAbility, float>> abilityFactorDicList,
        Virus.Type type)
    {
        abilityDicList.Clear();
        abilityFactorDicList.Clear();


    }

    public static void GetAttackAbility(
        List<Dictionary<Enemy.AttackAbility, float>> abilityDicList,
        List<Dictionary<Enemy.AttackAbility, float>> abilityFactorDicList,
        SpecialError.Type type)
    {
        abilityDicList.Clear();
        abilityFactorDicList.Clear();


    }
}
