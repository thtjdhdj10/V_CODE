using UnityEngine;
using System.Collections;

public class AimTimer : Unit {

    public float remainTime;
    public string createTarget;

    Animator animator;
    SpriteManager.SpriteAttribute sa;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();

        sa = SpriteManager.manager.typeSpriteDic
            [SpriteManager.Category.PARTICLE]
            [SpriteManager.Name.AIM]
            [SpriteManager.Status.NONE];
    }

    public void OnEnable()
    {
        if (remainTime > 0f)
        {
            SpriteManager.SetSpriteCycleTime(ref sa, remainTime);
        }
        else
        {
            remainTime = sa.cycle;
        }

        animator.speed = sa.speed;
    }

    void SetTimer(float time)
    {
        remainTime = time;

        SpriteManager.SetSpriteCycleTime(ref sa, remainTime);

        animator.speed = sa.speed;
    }

    void Update()
    {
        if(remainTime > 0f)
        {
            remainTime -= Time.deltaTime;
        }
        else
        {
            ActiveTimer();
        }
    }

    protected virtual void ActiveTimer()
    {
        GameObject obj = VEasyPoolerManager.GetObjectRequest(createTarget);

        obj.transform.position = transform.position;

        animator.speed = 0f;

        VEasyPoolerManager.ReleaseObjectRequest(gameObject);
    }
}
