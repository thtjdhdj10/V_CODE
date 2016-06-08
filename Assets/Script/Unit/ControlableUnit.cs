﻿using UnityEngine;
using System.Collections.Generic;

public class ControlableUnit : MonoBehaviour {

    public Player player;

    public bool[] moveDir = new bool[4];

    public float moveSpeed = 2f;

    public enum Direction
    {
        LEFT = 0,
        RIGHT,
        UP,
        DOWN,
    }

    Dictionary<Direction, KeyManager.Command> dirKeyDic = new Dictionary<Direction, KeyManager.Command>();
    Dictionary<Direction, Direction> dirRevdirDic = new Dictionary<Direction, Direction>();

    Vector2 moveDelta;

    public virtual void ReceiveCommand(KeyManager.Command command, KeyManager.KeyPressType type)
    {
        // 눌린 키와 대응되는 명령 command 와 눌리는 방식 type 에 대응되는 행동을 정의할 것.

        UpdateMoveState(command, type);

//        Debug.Log(gameObject.name + "->" + command + " " + type);
    }

    void Awake()
    {
        player = GetComponent<Player>();

        dirKeyDic[Direction.LEFT] = KeyManager.Command.MOVE_LEFT;
        dirKeyDic[Direction.RIGHT] = KeyManager.Command.MOVE_RIGHT;
        dirKeyDic[Direction.UP] = KeyManager.Command.MOVE_UP;
        dirKeyDic[Direction.DOWN] = KeyManager.Command.MOVE_DOWN;

        dirRevdirDic[Direction.LEFT] = Direction.RIGHT;
        dirRevdirDic[Direction.RIGHT] = Direction.LEFT;
        dirRevdirDic[Direction.UP] = Direction.DOWN;
        dirRevdirDic[Direction.DOWN] = Direction.UP;
    }

    void Update()
    {
        MoveFrame();


    }

    void UpdateMoveState(KeyManager.Command command, KeyManager.KeyPressType type)
    {
        for (int d = 0; d < 4; ++d)
        {
            Direction dir = (Direction)d;
            if (command == dirKeyDic[dir])
            {
                if (type == KeyManager.KeyPressType.DOWN)
                {
                    moveDir[(int)dir] = true;

                    moveDir[(int)dirRevdirDic[dir]] = false;
                }
                else if (type == KeyManager.KeyPressType.PRESS)
                {
                    if (moveDir[(int)dirRevdirDic[dir]] == false)
                    {
                        moveDir[(int)dir] = true;
                    }
                }
                else if (type == KeyManager.KeyPressType.UP)
                {
                    moveDir[(int)dir] = false;
                }

                return;
            }
        }
    }

    void MoveFrame()
    {
	    int dirCount = 0;

	    for (int d = 0; d < 4; ++d)
	    {
            if (moveDir[d] == true)
		    {
			    dirCount++;
		    }
	    }

        float moveDelta = moveSpeed * Time.deltaTime;

	    if (dirCount >= 2)
	    {
            moveDelta /= Mathf.Sqrt(2f);
	    }

        Vector2 delta = new Vector2(0f, 0f);
        if(moveDir[(int)Direction.LEFT] == true)
        {
            delta.x -= moveDelta;
        }
        else if (moveDir[(int)Direction.RIGHT] == true)
        {
            delta.x += moveDelta;
        }
        if(moveDir[(int)Direction.UP] == true)
        {
            delta.y += moveDelta;
        }
        else if(moveDir[(int)Direction.DOWN] == true)
        {
            delta.y -= moveDelta;
        }

        player.ShiftPosition(delta);
    }

}
