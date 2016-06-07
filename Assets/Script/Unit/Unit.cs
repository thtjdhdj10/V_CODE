using UnityEngine;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{
    public enum RelationsWithPlayer
    {
        NONE = 0,
        ALLY = 1,
        ENEMY = 2,
    }

    public enum Status
    {
        // TODO unit 의 하위클래스에 enemy, error 
        HEALTHY,
        
    }

    //

    public class Ability
    {
        public float attackPoint;
        public float defencePoint;
        public float currentHp;
        public float currentMp;
    }

    public class AttackAbility
    {
        public float minAttackRange; // default : 0
        public float attackStartRange; // default : basicAttackRange
        public float basicAttackRange;

        public float searchRange; // default : sight

        public float attackDelay;
    }

    public class ExtraAbility
    {
        // property of attack/heal
        public float importance;

        public float sight;

        public float logicalSize;

        public float moveSpeed;
    }

    public Ability originalAbility = new Ability();
    public Ability currentAbility = new Ability();

    public AttackAbility originalAttackAbility = new AttackAbility();
    public AttackAbility currentAttackAbility = new AttackAbility();

    public ExtraAbility originalExtraAbility = new ExtraAbility();
    public ExtraAbility currentExtraAbility = new ExtraAbility();

    public void Init()
    {
        currentAbility = originalAbility;
        currentAttackAbility = originalAttackAbility;
        currentExtraAbility = originalExtraAbility;
    }

    public void CompleteInit()
    {
        // model object 이용
    }

    //

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void MoveToPosition()
    {

    }


}
