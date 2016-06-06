using UnityEngine;
using System.Collections;

public class MovePlayer : ControlableUnit {

    public enum MoveType
    {
        APPLY_SPEED,
        ADD_SPEED,

    }

    public MoveType moveType;

    public float maxSpeed;


    public virtual void ReceiveCommand(KeyManager.Command command, KeyManager.KeyPressType type)
    {
        // 눌린 키와 대응되는 명령 command 와 눌리는 방식 type 에 대응되는 행동을 정의할 것.

        if (type == KeyManager.KeyPressType.PRESS)
            return;
        
        Debug.Log(gameObject.name + "->" + command + " " + type);
    }

    void FixedUpdate()
    {
        MoveFrame();
    }

    void MoveFrame()
    {
//          Time.fixedDeltaTime
    }
}
