using UnityEngine;
using System.Collections.Generic;

public class MovingUnit : OperateUnit
{
    protected override void Awake()
    {
        base.Awake();

        movingUnitList.Add(this);

        MoveTypeFrameDic[MoveType.STRAIGHT] = StraightMove;
        MoveTypeFrameDic[MoveType.REGULAR_CURVE] = RegularCurveMove;
        MoveTypeFrameDic[MoveType.REGULAR_CURVE_PER_DISTANCE] = RegularCurvePerDistanceMove;
        MoveTypeFrameDic[MoveType.LERP_CURVE] = LerpCurveMove;
        MoveTypeFrameDic[MoveType.LERP_CURVE_PER_DISTANCE] = LerpCurvePerDistanceMove;
    }

    public MoveType moveType;

    public Unit target;

    public float speed;

    public float direction;

    public float curveFactor;

    public float maxCurveDistance;

    public float minCurveDistance;

    public float distanceFactor;

    public BounceType bounceType;

    public enum MoveType
    {
        NONE,
        STRAIGHT,
        REGULAR_CURVE,
        REGULAR_CURVE_PER_DISTANCE,
        LERP_CURVE,
        LERP_CURVE_PER_DISTANCE,
    }

    public enum BounceType
    {
        NONE,
        BOUNCE_WALL,
        BOUNCE_UNIT,
        BOUNCE_ENEMY,
    }

    //

    public void InitStraightMove(float spd, float dir, BounceType _bounceType)
    {
        moveType = MoveType.STRAIGHT;
        speed = spd;
        direction = dir;
        bounceType = _bounceType;

        SetSpriteAngle();
    }

    public void InitRegularCurvePerDistanceMove(float spd, float dir, Unit tar, float curve, float maxDis, float minDis, float disFactor)
    {
        moveType = MoveType.REGULAR_CURVE_PER_DISTANCE;
        target = tar;

        speed = spd;
        direction = dir;
        curveFactor = curve;
        maxCurveDistance = maxDis;
        minCurveDistance = minDis;
        distanceFactor = disFactor;
    }

    public void InitLerpCurve(float spd, float dir, Unit tar, float curve, BounceType _bounceType)
    {
        moveType = MoveType.LERP_CURVE;
        target = tar;

        speed = spd;
        direction = dir;
        curveFactor = curve;
        bounceType = _bounceType;
    }

    //

    delegate void MoveFrame();

    Dictionary<MoveType, MoveFrame> MoveTypeFrameDic = new Dictionary<MoveType, MoveFrame>();

    public void SetSpriteAngle()
    {
        Vector3 rot = transform.eulerAngles;
        rot.z = direction + SpriteManager.spriteDefaultRotation;
        transform.eulerAngles = rot;
    }

    public virtual void FixedUpdate()
    {
        if (MoveTypeFrameDic.ContainsKey(moveType) == true)
        {
            MoveTypeFrameDic[moveType]();
            CollisionProcessing();
        }
    }

    void CollisionProcessing()
    {
        switch(bounceType)
        {
            case BounceType.BOUNCE_WALL:
                {
                    if (owner.CheckTerritory() != PlayerMove.Direction.NONE)
                    {
                        float dirToPlayer = VEasyCalculator.GetDirection(owner, Player.player);
                        direction = dirToPlayer;
                    }
                }
                break;
            case BounceType.BOUNCE_UNIT:
                {
                    if(owner.hittableUnit != null)
                    {
                        Unit colUnit = owner.hittableUnit.CollisionCheck(false);

                        if(colUnit != null)
                        {
                            direction += 180f;
                            VEasyCalculator.GetNormalizedDirection(ref direction);
                        }
                    }
                }
                break;
            case BounceType.BOUNCE_ENEMY:
                {
                    if (owner.hittableUnit != null)
                    {
                        Unit colUnit = owner.hittableUnit.CollisionCheck();

                        if (colUnit != null)
                        {
                            direction += 180f;
                            VEasyCalculator.GetNormalizedDirection(ref direction);
                        }
                    }
                }
                break;
        }
    }

    protected virtual void StraightMove()
    {
        float moveDistance = speed * Time.fixedDeltaTime;

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(direction, moveDistance);
        Vector2 v2Pos = owner.transform.position;
        transform.position = v2Pos + moveVector;
    }
    
    protected virtual void LerpCurveMove()
    {
        float moveDistance = speed * Time.fixedDeltaTime;

        float dirToPlayer = VEasyCalculator.GetDirection(owner.transform.position, target.transform.position);

        float disToPlayer = Vector2.Distance(target.transform.position, owner.transform.position);

        direction = VEasyCalculator.GetLerpDirection(
            direction, dirToPlayer, curveFactor * Time.fixedDeltaTime);

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(direction, moveDistance);

        Vector2 v2Pos = owner.transform.position;
        owner.transform.position = v2Pos + moveVector;
    }

    protected virtual void LerpCurvePerDistanceMove()
    {
        float moveDistance = speed * Time.fixedDeltaTime;

        float dirToPlayer = VEasyCalculator.GetDirection(owner.transform.position, target.transform.position);

        float disToPlayer = Vector2.Distance(target.transform.position, owner.transform.position);

        direction = VEasyCalculator.GetLerpDirection(
            direction, dirToPlayer, curveFactor * Time.fixedDeltaTime,
            maxCurveDistance, minCurveDistance, disToPlayer, distanceFactor);

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(direction, moveDistance);

        Vector2 v2Pos = owner.transform.position;
        owner.transform.position = v2Pos + moveVector;
    }

    protected virtual void RegularCurveMove()
    {
        float moveDistance = speed * Time.fixedDeltaTime;

        float dirToPlayer = VEasyCalculator.GetDirection(owner.transform.position, target.transform.position);

        float disToPlayer = Vector2.Distance(target.transform.position, owner.transform.position);

        direction = VEasyCalculator.GetTurningDirection(
            direction, dirToPlayer, curveFactor * Time.fixedDeltaTime);

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(direction, moveDistance);

        Vector2 v2Pos = owner.transform.position;
        owner.transform.position = v2Pos + moveVector;
    }

    protected virtual void RegularCurvePerDistanceMove()
    {
        float moveDistance = speed * Time.fixedDeltaTime;

        float dirToPlayer = VEasyCalculator.GetDirection(owner.transform.position, target.transform.position);

        float disToPlayer = Vector2.Distance(target.transform.position, owner.transform.position);

        direction = VEasyCalculator.GetTurningDirection(
            direction, dirToPlayer, curveFactor * Time.fixedDeltaTime,
            maxCurveDistance, minCurveDistance, disToPlayer, distanceFactor * Time.fixedDeltaTime);

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(direction, moveDistance);

        Vector2 v2Pos = owner.transform.position;
        owner.transform.position = v2Pos + moveVector;
    }
}
