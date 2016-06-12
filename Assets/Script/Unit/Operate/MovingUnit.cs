using UnityEngine;
using System.Collections.Generic;

public class MovingUnit : OperateUnit
{
    protected virtual void Awake()
    {
        movingUnitList.Add(this);

        MoveTypeFrameDic[MoveType.STRAIGHT] = StraightMove;
        MoveTypeFrameDic[MoveType.REGULAR_CURVE] = RegularCurveMove;
        MoveTypeFrameDic[MoveType.REGULAR_CURVE_PER_DISTANCE] = RegularCurvePerDistanceMove;
        MoveTypeFrameDic[MoveType.LERP_CURVE] = LerpCurveMove;
        MoveTypeFrameDic[MoveType.LERP_CURVE_PER_DISTANCE] = LerpCurvePerDistanceMove;
    }

    public MoveType moveType;

    public Unit target;

    public float moveSpeed;

    public float moveDirection;

    public float curveFactor;

    public float maxCurveDistance;

    public float minCurveDistance;

    public float distanceFactor;

    public enum MoveType
    {
        NONE,
        STRAIGHT,
        REGULAR_CURVE,
        LERP_CURVE,
        REGULAR_CURVE_PER_DISTANCE,
        LERP_CURVE_PER_DISTANCE,
    }

    delegate void MoveFrame();

    Dictionary<MoveType, MoveFrame> MoveTypeFrameDic = new Dictionary<MoveType, MoveFrame>();

    public virtual void FixedUpdate()
    {
        if (MoveTypeFrameDic.ContainsKey(moveType) == true)
            MoveTypeFrameDic[moveType]();


    }

    protected virtual void StraightMove()
    {
        float moveDistance = moveSpeed * Time.fixedDeltaTime;

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(moveDirection, moveDistance);
        Vector2 v2Pos = owner.transform.position;
        transform.position = v2Pos + moveVector;
    }
    
    protected virtual void LerpCurveMove()
    {
        float moveDistance = moveSpeed * Time.fixedDeltaTime;

        float dirToPlayer = VEasyCalculator.GetDirection(owner.transform.position, target.transform.position);

        float disToPlayer = Vector2.Distance(target.transform.position, owner.transform.position);

        moveDirection = VEasyCalculator.GetLerpDirection(
            moveDirection, dirToPlayer, curveFactor * Time.fixedDeltaTime);

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(moveDirection, moveDistance);

        Vector2 v2Pos = owner.transform.position;
        owner.transform.position = v2Pos + moveVector;
    }

    protected virtual void LerpCurvePerDistanceMove()
    {
        float moveDistance = moveSpeed * Time.fixedDeltaTime;

        float dirToPlayer = VEasyCalculator.GetDirection(owner.transform.position, target.transform.position);

        float disToPlayer = Vector2.Distance(target.transform.position, owner.transform.position);

        moveDirection = VEasyCalculator.GetLerpDirection(
            moveDirection, dirToPlayer, curveFactor * Time.fixedDeltaTime,
            maxCurveDistance, minCurveDistance, disToPlayer, distanceFactor);

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(moveDirection, moveDistance);

        Vector2 v2Pos = owner.transform.position;
        owner.transform.position = v2Pos + moveVector;
    }

    protected virtual void RegularCurveMove()
    {
        float moveDistance = moveSpeed * Time.fixedDeltaTime;

        float dirToPlayer = VEasyCalculator.GetDirection(owner.transform.position, target.transform.position);

        float disToPlayer = Vector2.Distance(target.transform.position, owner.transform.position);

        moveDirection = VEasyCalculator.GetTurningDirection(
            moveDirection, dirToPlayer, curveFactor * Time.fixedDeltaTime);

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(moveDirection, moveDistance);

        Vector2 v2Pos = owner.transform.position;
        owner.transform.position = v2Pos + moveVector;
    }

    protected virtual void RegularCurvePerDistanceMove()
    {
        float moveDistance = moveSpeed * Time.fixedDeltaTime;

        float dirToPlayer = VEasyCalculator.GetDirection(owner.transform.position, target.transform.position);

        float disToPlayer = Vector2.Distance(target.transform.position, owner.transform.position);

        moveDirection = VEasyCalculator.GetTurningDirection(
            moveDirection, dirToPlayer, curveFactor * Time.fixedDeltaTime,
            maxCurveDistance, minCurveDistance, disToPlayer, distanceFactor * Time.fixedDeltaTime);

        Vector2 moveVector = VEasyCalculator.GetRotatedPosition(moveDirection, moveDistance);

        Vector2 v2Pos = owner.transform.position;
        owner.transform.position = v2Pos + moveVector;
    }
}
