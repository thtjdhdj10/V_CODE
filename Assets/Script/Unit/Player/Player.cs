using UnityEngine;
using System.Collections;
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

        colType = Unit.ColliderType.CIRCLE;
        colCircle = 0.1f;

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

            GameObject obj = VEasyPoolerManager.GetFiniteParticleRequest("ParticleShieldBreak");

            float direction = VEasyCalculator.GetDirection(player, target);

            Vector3 angle;
            angle.x = -direction;// SpriteManager.spriteDefaultRotation;
            angle.y = 90f;
            angle.z = 0f;
            obj.transform.eulerAngles = angle;

            obj.transform.position = transform.position;

            StartCoroutine(ShieldBreakAnimation());
        }

        Debug.Log(target.name + " Hit Player");
    }

    IEnumerator ShieldBreakAnimation()
    {
        Color c = shield.color;
        Vector3 s = shield.transform.localScale;

        float beDisableTime = 0.75f;
        float passedTime = 0f;
        float timeRatio = passedTime / beDisableTime;

        float maxScale = 10f;

        float scale;
        float alpha;

        while(passedTime < beDisableTime)
        {
            passedTime += Time.deltaTime;
            timeRatio = passedTime / beDisableTime;

            alpha = (1f - timeRatio) * 0.5f;
            c.a = alpha;
            shield.color = c;

            scale = 1.2f + Mathf.Pow(0.0f + timeRatio * 1f, 10f) * maxScale;
            s.x = scale;
            s.y = scale;
            shield.transform.localScale = s;

            yield return new WaitForEndOfFrame();
        }

        shield.gameObject.SetActive(false);

        c.a = 1f;
        shield.color = c;
        shield.transform.localScale = new Vector3(1f, 1f, 1f);

        yield break;
    }

    IEnumerator ShieldCreateAnimation()
    {
        shield.gameObject.SetActive(true);

        Color c = shield.color;
        Vector3 s = shield.transform.localScale;

        float beDisableTime = 0.5f;
        float passedTime = 0f;
        float timeRatio = passedTime / beDisableTime;

        float maxScale = 3f;

        float scale;
        float alpha;

        while (passedTime < beDisableTime)
        {
            passedTime += Time.deltaTime;
            timeRatio = 1f - passedTime / beDisableTime;

            alpha = (1f - timeRatio) * 0.5f;
            c.a = alpha;
            shield.color = c;

            scale = 1.05f + Mathf.Pow(0.25f + timeRatio * 0.75f, 5f) * maxScale;
            s.x = scale;
            s.y = scale;
            shield.transform.localScale = s;

            yield return new WaitForEndOfFrame();
        }

        c.a = 1f;
        shield.color = c;
        shield.transform.localScale = new Vector3(1f, 1f, 1f);

        yield break;
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

                StartCoroutine(ShieldCreateAnimation());
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
                    pos.x = -((float)Screen.width / (float)Screen.height) * 2f + colCircle * 1.1f;
                }
                break;
            case PlayerMove.Direction.RIGHT:
                {
                    pos.x = ((float)Screen.width / (float)Screen.height) * 2f - colCircle * 1.1f;
                }
                break;
            case PlayerMove.Direction.DOWN:
                {
                    pos.y = -2f + colCircle * 1.1f;
                }
                break;
            case PlayerMove.Direction.UP:
                {
                    pos.y = 2f - colCircle * 1.1f;
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
