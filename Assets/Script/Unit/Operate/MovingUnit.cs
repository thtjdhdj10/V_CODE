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

    public enum MoveType
    {
        NONE,
        STRAIGHT,
        REGULAR_CURVE,
        REGULAR_CURVE_PER_DISTANCE,
        LERP_CURVE,
        LERP_CURVE_PER_DISTANCE,
    }

    //

    public void InitStraightMove(float spd, float dir)
    {
        moveType = MoveType.STRAIGHT;
        speed = spd;
        direction = dir;

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
            MoveTypeFrameDic[moveType]();


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
