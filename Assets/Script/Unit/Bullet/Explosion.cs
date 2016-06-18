using UnityEngine;
using System.Collections;

public class Explosion : DefaultBullet {

    public enum Type
    {
        NONE,
        BAZOOKA,

    }

    public Type type;

    float remainTime;

    Animator animator;
    SpriteManager.SpriteAttribute sa;

    protected override void Awake()
    {
        base.Awake();

        OperateComponentInit(true, false, false, false, false);

        animator = GetComponent<Animator>();

        switch(type)
        {
            case Type.BAZOOKA:
                {
                    sa = SpriteManager.manager.typeSpriteDic[SpriteManager.Category.PARTICLE][SpriteManager.Name.BAZOOKA][SpriteManager.Status.EXPLOSION];

                    logicalSize = 0.25f;
                }
                break;
            default:
                {
                    animator.speed = 0f;

                    VEasyPoolerManager.ReleaseObjectRequest(gameObject);
                }
                break;
        }
    }

    public void OnEnable()
    {
        remainTime = sa.cycle;

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
            animator.speed = 0f;

            VEasyPoolerManager.ReleaseObjectRequest(gameObject);
        }
    }

    public override void Hit(Unit target)
    {

    }
}
