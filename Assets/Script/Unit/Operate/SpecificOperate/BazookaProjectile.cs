using UnityEngine;
using System.Collections;

public class BazookaProjectile : ProjectileUnit {

    IEnumerator AnalyzingAttack(BazookaAttack bullet)
    {
        SpriteManager.SpriteAttribute sa = SpriteManager.manager.typeSpriteDic
            [SpriteManager.Category.PARTICLE]
            [SpriteManager.Name.AIM]
            [SpriteManager.Status.NONE];

        Vector2 playerPos = Player.player.transform.position;

        float dir = VEasyCalculator.GetDirection(owner, Player.player);
        float prevDir = dir;

        float dis = Vector2.Distance(owner.transform.position, Player.player.transform.position);
        float prevDis = dis;

        float targetDir = dir;
        float targetDis = dis;

        Vector3 rot = transform.eulerAngles;
        rot.z = dir;
        transform.eulerAngles = rot;

        float passedTime = 0f;
        while(passedTime < bullet.analyzeTime)
        {
            passedTime += Time.deltaTime;

            playerPos = Player.player.transform.position;

            dir = VEasyCalculator.GetDirection(owner, Player.player);
            dis = Vector2.Distance(owner.transform.position, Player.player.transform.position);

            targetDir = VEasyCalculator.EstimateCollidingDirection(dir, prevDir, passedTime, sa.cycle + 0.2f);
            targetDis = VEasyCalculator.EstimateCollidingDistance(dis, prevDis, passedTime, sa.cycle + 0.2f);

            //targetPos = VEasyCalculator.EstimateCollidingPosition(playerPos, playerPrevPos, passedTime, sa.cycle + 0.3f);

            rot = transform.eulerAngles;
            rot.z = targetDir + SpriteManager.spriteDefaultRotation;
            transform.eulerAngles = rot;

            yield return new WaitForEndOfFrame();
        }

        Vector3 targetPos = VEasyCalculator.GetRotatedPosition(targetDir, targetDis);
        targetPos += owner.transform.position;

        GameObject obj = VEasyPoolerManager.GetObjectRequest(bullet.modelName);
        obj.transform.position = targetPos;
        obj.transform.localScale = new Vector3(bullet.explosionRange, bullet.explosionRange, bullet.explosionRange);

        Unit unit = obj.GetComponent<Unit>();
        unit.colType = Unit.ColliderType.CIRCLE;
        unit.colCircle = 0.25f * bullet.explosionRange;

        yield break;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override bool ActivePattern(int idx)
    {
        //if (activePatternList[idx].GetType() != typeof(BazookaAttack))
        //    return false;

        BazookaAttack bullet = activePatternList[idx] as BazookaAttack;

        StartCoroutine(AnalyzingAttack(bullet));

        return true;
    }
}
