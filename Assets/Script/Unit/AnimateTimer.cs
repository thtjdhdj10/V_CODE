using UnityEngine;
using System.Collections;

public class AnimateTimer : Unit {

    public float remainTime;
    public string createTarget;

    Animator animator;
    SpriteManager.SpriteAttribute sa;

    public enum Type
    {
        NONE,
        AIM,
        RUPTURE,
        SCRATCH,
    }

    public Type type;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();

        switch(type)
        {
            case Type.AIM:
                {
                    sa = SpriteManager.manager.typeSpriteDic
                        [SpriteManager.Category.PARTICLE]
                        [SpriteManager.Name.AIM]
                        [SpriteManager.Status.NONE];
                }
                break;
            case Type.RUPTURE:
                {
                    sa = SpriteManager.manager.typeSpriteDic
                        [SpriteManager.Category.PARTICLE]
                        [SpriteManager.Name.RUPTURE]
                        [SpriteManager.Status.INIT];
                }
                break;
            case Type.SCRATCH:
                {
                    sa = SpriteManager.manager.typeSpriteDic
                        [SpriteManager.Category.PARTICLE]
                        [SpriteManager.Name.SCRATCH]
                        [SpriteManager.Status.INIT];
                }
                break;
            default:
                {
                    animator.speed = 0f;

                    VEasyPoolerManager.ReleaseObjectRequest(gameObject);
                }
                return;
        }
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
