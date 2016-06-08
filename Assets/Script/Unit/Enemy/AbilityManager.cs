using UnityEngine;
using System.Collections.Generic;

public class AbilityManager {

    public static Dictionary<Enemy.Ability,float> GetAbility(CustomError.Body body, CustomError.Weapon weapon)
    {
        // TODO
        return new Dictionary<Enemy.Ability, float>();
    }

    public static Dictionary<Enemy.Ability, float> GetAbility(Virus.Type type)
    {
        return new Dictionary<Enemy.Ability, float>();
    }

    public static Dictionary<Enemy.Ability,float> GetAbility(SpecialError.Type type)
    {
        return new Dictionary<Enemy.Ability, float>();
    }
}
