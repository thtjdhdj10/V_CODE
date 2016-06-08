using UnityEngine;
using System.Collections.Generic;

// pure virtual class
public abstract class Enemy : Unit
{

    public virtual void Init()
    {
        AbilityInit();
    }

    //

    public List<SpriteRenderer> spriteList = new List<SpriteRenderer>();

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
        float hpRatio = currentAbilityDic[Ability.HEALTH_POINT] / originalAbilityDic[Ability.HEALTH_POINT];
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

    public enum Ability
    {
        HEALTH_POINT = 0,
        IMPORTANCE,
        LOGICAL_SIZE,
        MOVE_SPEED,
        POWER, // * factor
    }

    //public class AttackAbility
    //{
    //    public float attackPoint;
    //    public float attackPointFactor;

    //    public float minAttackRange; // default : 0
    //    public float minAttackRangeFactor;
    //    public float attackStartRange; // default : basicAttackRange
    //    public float attackStartRangeFactor;
    //    public float basicAttackRange;
    //    public float basicAttackRangeFactor;

    //    public float attackDelay;
    //    public float attackDelayFactor;
    //}

    public Dictionary<Ability, float> originalAbilityDic = new Dictionary<Ability,float>();
    public Dictionary<Ability, float> abilityFactorDic = new Dictionary<Ability,float>();
    public Dictionary<Ability, float> currentAbilityDic = new Dictionary<Ability,float>();

    //public Ability originalAbilityDic = new Ability();
    //public Ability currentAbilityDic = new Ability();

    //public AttackAbility originalAttackAbility = new AttackAbility();
    //public AttackAbility currentAttackAbility = new AttackAbility();

    public void AbilityInit()
    {
        foreach (var ability in originalAbilityDic.Keys)
        {
            float factor = 0f;

            if (ability != Ability.POWER &&
                abilityFactorDic.ContainsKey(ability) == true &&
                originalAbilityDic.ContainsKey(Ability.POWER) == true)
            {
                factor = abilityFactorDic[ability] * originalAbilityDic[Ability.POWER];
            }

            currentAbilityDic[ability] = originalAbilityDic[ability] + factor;
        }
    }

    public virtual void SetAbillity(CustomError.Body body, CustomError.Weapon weapon)
    {
        originalAbilityDic = AbilityManager.GetAbility(body, weapon);
        currentAbilityDic = AbilityManager.GetAbility(body, weapon);
    }

    public virtual void SetAbility(Virus.Type type)
    {
        originalAbilityDic = AbilityManager.GetAbility(type);
        currentAbilityDic = AbilityManager.GetAbility(type);
    }

    public virtual void SetAbility(SpecialError.Type type)
    {
        originalAbilityDic = AbilityManager.GetAbility(type);
        currentAbilityDic = AbilityManager.GetAbility(type);
    }

    public virtual void SetAbilityFactor()
    {

    }

    //

//    public virtual void Set
}
