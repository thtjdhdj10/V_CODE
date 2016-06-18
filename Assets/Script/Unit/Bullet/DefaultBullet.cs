using UnityEngine;
using System.Collections;

public class DefaultBullet : Unit {

    void Update()
    {
        if(CheckOutside() == true)
        {
            VEasyPoolerManager.ReleaseObjectRequest(gameObject);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        OperateComponentInit(true, false, true, false, false);
    }

    public override void Hit(Unit target)
    {
//        SpriteManager.SpriteAttribute sa = SpriteManager.manager.typeSpriteDic[SpriteManager.Category.PARTICLE][SpriteManager.Name.SPARK_1][SpriteManager.Status.NONE];

        //float lifeTime = sa.cycle;

        GameObject obj = VEasyPoolerManager.GetFiniteParticleRequest("ParticleBulletHit");

        //Animator animator = obj.GetComponent<Animator>();
        
        //animator.speed = sa.speed;

        Vector3 angle = transform.eulerAngles;
        angle.x = -angle.z + SpriteManager.spriteDefaultRotation;
        angle.y = 90f;
        angle.z = 0f;
        obj.transform.eulerAngles = angle;

        //Quaternion qt = Quaternion.EulerAngles(angle);

        obj.transform.position = transform.position;

        VEasyPoolerManager.ReleaseObjectRequest(gameObject);
    }
}
