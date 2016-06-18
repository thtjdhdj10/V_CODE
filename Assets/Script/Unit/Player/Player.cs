﻿using UnityEngine;
using System.Collections.Generic;

public class Player : Unit
{
    public static Player player;

    protected override void Awake()
    {
        base.Awake();

        unitActive = true;

        logicalSize = 0.1f;

        player = this;

        force = Force.PLAYER;
    }

    public void BeHit(Unit target)
    {
        Debug.Log(target.name + " Hit Player");
    }

    public void ShiftPosition(Vector2 delta)
    {
        Vector2 pos = transform.position + new Vector3(delta.x, delta.y, 0f);

        transform.position = pos;
    }

    void FixedUpdate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dirToMouse = VEasyCalculator.GetDirection(transform.position, mousePos);

        Vector3 rot = transform.eulerAngles;
        rot.z = dirToMouse + SpriteManager.spriteDefaultRotation;
        transform.eulerAngles = rot;
    }
}
