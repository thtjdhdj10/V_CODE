using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public bool unitActive = true;

    public enum ColliderType
    {
        NONE,
        CIRCLE,
        RECT,
    }

    public ColliderType colType;

    public float colCircle;

    public Vector2 colRect;

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

    public List<SpriteRenderer> spriteList = new List<SpriteRenderer>();

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

    public virtual void InitMovingModule()
    {

    }

    public virtual void InitAttackModule()
    {

    }

    public virtual void InitSprite()
    {

    }

    public virtual void Hit(Unit target)
    {

    }

    public bool CheckOutside()
    {
        Vector2 pos = transform.position;
        Rect rect = CameraManager.manager.GetLogicalRect();

        switch(colType)
        {
            case ColliderType.CIRCLE:
                {
                    if (pos.x + colCircle < rect.xMin ||
                        pos.x - colCircle > rect.xMax ||
                        pos.y + colCircle < rect.yMin ||
                        pos.y - colCircle > rect.yMax)
                    {
                        return true;
                    }
                }
                break;
            case ColliderType.RECT:
                {
                    if (pos.x + colRect.x < rect.xMin ||
                        pos.x - colRect.x > rect.xMax ||
                        pos.y + colRect.y < rect.yMin ||
                        pos.y - colRect.y > rect.yMax)
                    {
                        return true;
                    }
                }
                break;
        }

        return false;
    }

    public PlayerMove.Direction CheckTerritory()
    {
        Vector2 pos = transform.position;
        Rect rect = CameraManager.manager.GetLogicalRect();

        switch (colType)
        {
            case ColliderType.CIRCLE:
                {
                    if (pos.x - colCircle < rect.xMin)
                    {
                        return PlayerMove.Direction.LEFT;
                    }

                    if (pos.x + colCircle > rect.xMax)
                    {
                        return PlayerMove.Direction.RIGHT;
                    }

                    if (pos.y - colCircle < rect.yMin)
                    {
                        return PlayerMove.Direction.DOWN;
                    }

                    if (pos.y + colCircle > rect.yMax)
                    {
                        return PlayerMove.Direction.UP;
                    }
                }
                break;
            case ColliderType.RECT:
                {
                    if (pos.x - colRect.x < rect.xMin)
                    {
                        return PlayerMove.Direction.LEFT;
                    }

                    if (pos.x + colRect.x > rect.xMax)
                    {
                        return PlayerMove.Direction.RIGHT;
                    }

                    if (pos.y - colRect.y < rect.yMin)
                    {
                        return PlayerMove.Direction.DOWN;
                    }

                    if (pos.y + colRect.y > rect.yMax)
                    {
                        return PlayerMove.Direction.UP;
                    }
                    break;
                }
        }

        return PlayerMove.Direction.NONE;
    }

}
