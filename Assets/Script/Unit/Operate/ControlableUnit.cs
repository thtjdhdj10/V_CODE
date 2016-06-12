using UnityEngine;
using System.Collections.Generic;

public class ControlableUnit : OperateUnit
{
    protected virtual void Awake()
    {
        controlableUnitList.Add(this);
    }

    public virtual void ReceiveCommand(KeyManager.Command command, KeyManager.KeyPressType type)
    {
        // 눌린 키와 대응되는 명령 command 와 눌리는 방식 type 에 대응되는 행동을 정의할 것.

        //        Debug.Log(gameObject.name + "->" + command + " " + type);
    }


}
