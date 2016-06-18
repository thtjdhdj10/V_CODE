using UnityEngine;
using System.Collections.Generic;

public class PlayerAttack : ControlableUnit
{
    public enum AttackType
    {
        BULLET,
        MISSILE,
        FUNNEL,
    }

    private Dictionary<KeyManager.Command, AttackType> keyAttackTypeDic = new Dictionary<KeyManager.Command, AttackType>();
    public Dictionary<AttackType, bool> attackTypeStateDic = new Dictionary<AttackType, bool>();

    protected override void Awake()
    {
        base.Awake();

        keyAttackTypeDic[KeyManager.Command.COMMAND_ATTACK] = AttackType.BULLET; // mouse left
        keyAttackTypeDic[KeyManager.Command.COMMAND_SPECIAL] = AttackType.MISSILE; // mouse right
        keyAttackTypeDic[KeyManager.Command.COMMAND_SKILL] = AttackType.FUNNEL; // space bar

        attackTypeStateDic[AttackType.BULLET] = false;
        attackTypeStateDic[AttackType.MISSILE] = false;
        attackTypeStateDic[AttackType.FUNNEL] = false;
    }

    delegate void UpdateAttackState(KeyManager.KeyPressType type);

    public override void ReceiveCommand(KeyManager.Command command, KeyManager.KeyPressType pressType)
    {
        if(keyAttackTypeDic.ContainsKey(command) == true)
        {
            AttackType at = keyAttackTypeDic[command];

            if(pressType == KeyManager.KeyPressType.DOWN ||
                pressType == KeyManager.KeyPressType.PRESS)
            {
                attackTypeStateDic[at] = true;
            }
            else if(pressType == KeyManager.KeyPressType.UP)
            {
                attackTypeStateDic[at] = false;
            }
        }

        //if(command == KeyManager.Command.COMMAND_ATTACK &&
        //    pressType == KeyManager.KeyPressType.DOWN)
        //{
        //    VEasyPoolerManager.GetObjectRequest("ExplosionTimer");
        //}
    }
}
