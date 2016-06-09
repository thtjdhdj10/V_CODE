using UnityEngine;
using System.Collections.Generic;

// pure virtual class
public abstract class Enemy : Unit
{
    public float power = 0f;

    public virtual void Init()
    {

    }

    //

    public List<SpriteRenderer> spriteList = new List<SpriteRenderer>();

    [SerializeField]
    public Dictionary<BasicAbility, float> originalAbilityDic = new Dictionary<BasicAbility, float>();
    public Dictionary<BasicAbility, float> abilityFactorDic = new Dictionary<BasicAbility, float>();
    public Dictionary<BasicAbility, float> currentAbilityDic = new Dictionary<BasicAbility, float>();

    public List<Dictionary<AttackAbility, float>> originalAttackAbilityDicList = new List<Dictionary<AttackAbility, float>>();
    public List<Dictionary<AttackAbility, float>> attackAbilityFactorDicList = new List<Dictionary<AttackAbility, float>>();
    public List<Dictionary<AttackAbility, float>> currentAttackAbilityDicList = new List<Dictionary<AttackAbility, float>>();

    //

    public enum Health
    {
        DEAD = 0,
        QUARTER = 1,
        HALF = 2,
        THREE_QUARTER = 3,
        FULL = 4,
    }
    public Health GetHealth()
    {
        float hpRatio = currentAbilityDic[BasicAbility.HEALTH_POINT] / originalAbilityDic[BasicAbility.HEALTH_POINT];
        if (hpRatio <= 0f)
            return Health.DEAD;
        else if (hpRatio < 0.25f)
            return Health.QUARTER;
        else if (hpRatio < 0.5f)
            return Health.HALF;
        else if (hpRatio < 0.75f)
            return Health.THREE_QUARTER;
        return Health.FULL;
    }

    protected Dictionary<Health, Color> healthColorDic = new Dictionary<Health, Color>();
    protected virtual void SetColorPerHealth()
    {
        healthColorDic[Health.DEAD] = Color.white;
        healthColorDic[Health.QUARTER] = Color.white;
        healthColorDic[Health.HALF] = Color.white;
        healthColorDic[Health.THREE_QUARTER] = Color.white;
        healthColorDic[Health.FULL] = Color.white;
    }

    protected void UpdateSpriteColor()
    {
        Color c = healthColorDic[GetHealth()];
        for (int i = 0; i < spriteList.Count; ++i)
        {
            spriteList[i].color = c;
        }
    }

    //

    // update attack delay

    //

    // 이 값은 모든 Enemy 에서 사용되는 공통적인 속성만 포함시킬 것.
    public enum BasicAbility
    {
        HEALTH_POINT = 0,
        IMPORTANCE,
        LOGICAL_SIZE,
        MOVE_SPEED,
        TURN_AMOUNT,
    }

    public enum AttackAbility
    {
        //ATTACK_DAMAGE = 0,
        ATTACK_DELAY = 0,
        TRIGGER_DELAY,
        PROJECTILE_SPEED,
        ACCELERATION_PER_SEC,
        EXPLOSION_RANGE,
        TURN_AMOUNT,
        MIN_ATTACK_RANGE,
        MAX_ATTACK_RANGE,
        RELATIVE_POSITION_X,
        RELATIVE_POSITION_Y,
        ATTACK_DIRECTION,
    }

    //public void ResetBasicAbility(Dictionary<BasicAbility, float> abilityDic)
    //{
    //    abilityDic[BasicAbility.HEALTH_POINT] = 0f;
    //    abilityDic[BasicAbility.IMPORTANCE] = 0f;
    //    abilityDic[BasicAbility.LOGICAL_SIZE] = 0f;
    //    abilityDic[BasicAbility.MOVE_SPEED] = 0f;
    //    abilityDic[BasicAbility.TURN_AMOUNT] = 0f;
    //}

    //public void ResetAttackAbility(Dictionary<AttackAbility, float> abilityDic)
    //{
    //    abilityDic[AttackAbility.ATTACK_DAMAGE] = 0f;
    //    abilityDic[AttackAbility.MIN_ATTACK_RANGE] = 0f;
    //    abilityDic[AttackAbility.ATTACK_START_RANGE] = 0f;
    //    abilityDic[AttackAbility.MAX_ATTACK_RANGE] = 0f;
    //    abilityDic[AttackAbility.ATTACK_DELAY] = 0f;
    //    abilityDic[AttackAbility.ATTACK_DIRECTION] = 0f;
    //    abilityDic[AttackAbility.RELATIVE_POSITION_X] = 0f;
    //    abilityDic[AttackAbility.RELATIVE_POSITION_Y] = 0f;
    //}

    //public void ResetAttackAbility(List<Dictionary<AttackAbility, float>> abilityDicList)
    //{
    //    for (int i = 0; i < abilityFactorDic.Count; ++i)
    //    {
    //        ResetAttackAbility(abilityDicList[i]);
    //    }
    //}

    public void CurrentBasicAbilityInit(float power)
    {
        currentAbilityDic.Clear();

        foreach (var ability in originalAbilityDic.Keys)
        {
            float factor = 0f;

            if (abilityFactorDic.ContainsKey(ability) == true)
            {
                factor = abilityFactorDic[ability] * power;
            }

            currentAbilityDic[ability] = originalAbilityDic[ability] + factor;
        }
    }

    public void CurrentAttackAbilityInit(float power)
    {
        currentAttackAbilityDicList.Clear();

        for (int i = 0; i < originalAttackAbilityDicList.Count; ++i)
        {
            currentAttackAbilityDicList.Add(new Dictionary<AttackAbility,float>());

            foreach (var ability in originalAttackAbilityDicList[i].Keys)
            {
                float factor = 0f;

                if (attackAbilityFactorDicList[i].ContainsKey(ability) == true)
                {
                    if (ability == AttackAbility.ATTACK_DELAY)
                    {
                        // power = 1, factor = 0.25 일 때 80% delay
                        factor = originalAttackAbilityDicList[i][ability] /
                            (1 + attackAbilityFactorDicList[i][ability] * power)
                            - originalAttackAbilityDicList[i][ability];
                    }
                    factor = attackAbilityFactorDicList[i][ability] * power;
                }

                currentAttackAbilityDicList[i][ability] = attackAbilityFactorDicList[i][ability] + factor;
            }
        }
    }

    //

    // Enemy 의 기본 속성을 초기화
    public virtual void SetAbillity(float power, CustomError.Body body, CustomError.Weapon weapon)
    {
        AbilityManager.GetAbility(originalAbilityDic, abilityFactorDic, body, weapon);
        CurrentBasicAbilityInit(power);

        AbilityManager.GetAttackAbility(originalAttackAbilityDicList, attackAbilityFactorDicList, body, weapon);
        CurrentAttackAbilityInit(power);
    }

    public virtual void SetAbility(float power, Virus.Type type)
    {
        AbilityManager.GetAbility(originalAbilityDic, abilityFactorDic, type);
        CurrentBasicAbilityInit(power);

        AbilityManager.GetAttackAbility(originalAttackAbilityDicList, attackAbilityFactorDicList, type);
        CurrentAttackAbilityInit(power);
    }

    public virtual void SetAbility(float power, SpecialError.Type type)
    {
        AbilityManager.GetAbility(originalAbilityDic, abilityFactorDic, type);
        CurrentBasicAbilityInit(power);

        AbilityManager.GetAttackAbility(originalAttackAbilityDicList, attackAbilityFactorDicList, type);
        CurrentAttackAbilityInit(power);
    }
}
