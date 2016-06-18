using UnityEngine;
using System.Collections.Generic;

public class Player : Unit
{
    public static Player player;

    public bool shieldOn;

    public float shieldChargeDelay;
    float passedChargeDelay;

    SpriteRenderer shield;

    protected override void Awake()
    {
        base.Awake();

        shieldOn = true;

        shieldChargeDelay = 4f;
        passedChargeDelay = 0f;

        unitActive = true;

        logicalSize = 0.1f;

        player = this;

        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; ++i)
        {
            if (sprites[i].name == "player shield")
            {
                shield = sprites[i];
                break;
            }
        }

        force = Force.PLAYER;
    }

    public void BeHit(Unit target)
    {
        if(shieldOn)
        {
            shieldOn = false;
            shield.gameObject.SetActive(false);

            GameObject obj = VEasyPoolerManager.GetFiniteParticleRequest("ParticleShieldBreak");

            float direction = VEasyCalculator.GetDirection(player, target);

            Vector3 angle;
            angle.x = -direction + SpriteManager.spriteDefaultRotation;
            angle.y = 90f;
            angle.z = 0f;
            obj.transform.eulerAngles = angle;

            obj.transform.position = transform.position;
        }

        Debug.Log(target.name + " Hit Player");
    }

    void Update()
    {
        if(shieldOn == false)
        {
            if (passedChargeDelay < shieldChargeDelay)
            {
                passedChargeDelay += Time.deltaTime;
            }
            else
            {
                passedChargeDelay = 0f;
                shieldOn = true;
                shield.gameObject.SetActive(true);
            }
        }
    }

    public void ShiftPosition(Vector2 delta)
    {
        Vector2 pos = transform.position + new Vector3(delta.x, delta.y, 0f);

        PlayerMove.Direction collisionDirection = CheckTerritory();
        switch(collisionDirection)
        {
            case PlayerMove.Direction.LEFT:
                {
                    pos.x = -((float)Screen.width / (float)Screen.height) * 2f + logicalSize * 1.1f;
                }
                break;
            case PlayerMove.Direction.RIGHT:
                {
                    pos.x = ((float)Screen.width / (float)Screen.height) * 2f - logicalSize * 1.1f;
                }
                break;
            case PlayerMove.Direction.DOWN:
                {
                    pos.y = -2f + logicalSize * 1.1f;
                }
                break;
            case PlayerMove.Direction.UP:
                {
                    pos.y = 2f - logicalSize * 1.1f;
                }
                break;
        }

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
