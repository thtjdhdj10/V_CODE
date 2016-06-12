using UnityEngine;
using System.Collections;

public class PlayerAttack : ControlableUnit
{
    Player player;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<Player>();


    }

    public override void ReceiveCommand(KeyManager.Command command, KeyManager.KeyPressType type)
    {
        UpdateAttackState(command, type);


    }

    void UpdateAttackState(KeyManager.Command command, KeyManager.KeyPressType type)
    {

    }
}
