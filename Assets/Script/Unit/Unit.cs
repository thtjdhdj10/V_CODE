using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public bool unitActive = true;

    public float logicalSize;

    [System.NonSerialized]
    public HittableUnit hittableUnit;
    [System.NonSerialized]
    public BeHittableUnit beHittableUnit;
    [System.NonSerialized]
    public MovingUnit movingUnit;
    [System.NonSerialized]
    public ControlableUnit controlableUnit;
    [System.NonSerialized]
    public ProjectileUnit projectileUnit;

    public enum Force
    {
        ENEMY,
        PLAYER,
        NONE,
    }

    public Force force = Force.ENEMY;

    //

    protected virtual void Awake()
    {
        hittableUnit = GetComponent<HittableUnit>();
        beHittableUnit = GetComponent<BeHittableUnit>();
        movingUnit = GetComponent<MovingUnit>();
        controlableUnit = GetComponent<ControlableUnit>();
        projectileUnit = GetComponent<ProjectileUnit>();
    }

    public void OperateComponentInit(bool hit, bool beHit, bool moving, bool control, bool projectile)
    {
        if (hit && hittableUnit == null)
            hittableUnit = gameObject.AddComponent<HittableUnit>();

        if (beHit && beHittableUnit == null)
            beHittableUnit = gameObject.AddComponent<BeHittableUnit>();

        if (moving && movingUnit == null)
            movingUnit = gameObject.AddComponent<MovingUnit>();

        if (control && controlableUnit == null)
            controlableUnit = gameObject.AddComponent<ControlableUnit>();

        if (projectile && projectileUnit == null)
            projectileUnit = gameObject.AddComponent<ProjectileUnit>();
    }

    public virtual void Init()
    {

    }

    public virtual void Hit(Unit target)
    {

    }

    protected bool CheckOutside()
    {
        Vector2 pos = transform.position;
        Rect rect = CameraManager.manager.GetLogicalRect();

        if(pos.x + logicalSize < rect.xMin||
            pos.x - logicalSize > rect.xMax ||
            pos.y + logicalSize < rect.yMin ||
            pos.y - logicalSize > rect.yMax)
        {
            return true;
        }

        return false;
    }

    protected PlayerMove.Direction CheckTerritory()
    {
        Vector2 pos = transform.position;
        Rect rect = CameraManager.manager.GetLogicalRect();

        if (pos.x - logicalSize < rect.xMin)
        {
            return PlayerMove.Direction.LEFT;
        }

        if (pos.x + logicalSize > rect.xMax)
        {
            return PlayerMove.Direction.RIGHT;
        }

        if (pos.y - logicalSize < rect.yMin)
        {
            return PlayerMove.Direction.DOWN;
        }

        if (pos.y + logicalSize > rect.yMax)
        {
            return PlayerMove.Direction.UP;
        }

        return PlayerMove.Direction.NONE;
    }

}
