using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// pure virtual class
public abstract class Enemy : Unit
{
    public float power = 0f;

    public void CreateUnit(float createTime)
    {
        unitActive = false;

        StartCoroutine(ActionCreateUnit(createTime));
    }

    public void ReleaseUnit(float releaseTime)
    {
        unitActive = false;

        StartCoroutine(ActionReleaseUnit(releaseTime));
    }

    protected virtual IEnumerator ActionCreateUnit(float createTime)
    {
        float remainTime = createTime;

        while (remainTime > 0f)
        {
            remainTime -= Time.deltaTime;

            float ratio = 1f - remainTime / createTime;
            ratio = 0.5f + ratio * 0.5f;

            for (int i = 0; i < spriteList.Count;++i )
            {
                Color color = spriteList[i].color;
                color.a = ratio;
                spriteList[i].color = color;
            }

            yield return new WaitForEndOfFrame();
        }

        unitActive = true;

        yield break;
    }

    protected virtual IEnumerator ActionReleaseUnit(float releaseTime)
    {
        float remainTime = releaseTime;

        while (remainTime > 0f)
        {
            remainTime -= Time.deltaTime;

            float ratio = remainTime / releaseTime;
            ratio = ratio * ratio;

            for (int i = 0; i < spriteList.Count; ++i)
            {
                Color color = spriteList[i].color;
                color.a = ratio;
                spriteList[i].color = color;
            }

            yield return new WaitForEndOfFrame();
        }

        unitActive = false;

        VEasyPoolerManager.ReleaseObjectRequest(gameObject);

        yield break;
    }

    //

    [SerializeField]
    public Dictionary<BasicAbility, float> originalAbilityDic = new Dictionary<BasicAbility, float>();
    public Dictionary<BasicAbility, float> abilityFactorDic = new Dictionary<BasicAbility, float>();
    public Dictionary<BasicAbility, float> currentAbilityDic = new Dictionary<BasicAbility, float>();

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
            float alpha = spriteList[i].color.a;

            c.a = alpha;
            spriteList[i].color = c;
        }
    }

    protected void SpriteSetting()
    {
        spriteList.Clear();

        SpriteRenderer[] sr = GetComponentsInChildren<SpriteRenderer>();
        spriteList.AddRange(sr);
    }

    //

    //

    // 이 값은 모든 Enemy 에서 사용되는 공통적인 속성만 포함시킬 것.
    public enum BasicAbility
    {
        HEALTH_POINT = 0,
        MOVE_SPEED,
        TURN_AMOUNT,
    }

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

    //

    // Enemy 의 기본 속성을 초기화
    public virtual void SetAbility(float power, CustomError.Body body, CustomError.Weapon weapon)
    {
        AbilityManager.GetAbility(originalAbilityDic, abilityFactorDic, body, weapon);
        CurrentBasicAbilityInit(power);
    }

    public virtual void SetAbility(float power, Virus.Type type)
    {
        AbilityManager.GetAbility(originalAbilityDic, abilityFactorDic, type);
        CurrentBasicAbilityInit(power);
    }

    public virtual void SetAbility(float power, SpecialError.Type type)
    {
        AbilityManager.GetAbility(originalAbilityDic, abilityFactorDic, type);
        CurrentBasicAbilityInit(power);
    }
}
