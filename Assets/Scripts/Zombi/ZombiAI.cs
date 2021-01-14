using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class ZombiAI : MonoBehaviour
{
    bool result = false;

    [Header("Radius Settings")]
    public float moveRadius = 10;
    public float attackRadius = 3;
    public float escapeRadius = 15;

    [Header("Attack Settings")]
    public int damageAttack = 1;
    public float attackTime = 2f;
    float nextAttack;



    [Header("Patrol Settings")]
    public bool imPatrol = true;
    public float speed;
    public Transform[] moveSpots;
    bool goBack = true;

    Player player;
    PlayerLife playerLife;
    Animator animator;



    AIPath aIPath;
    AIDestinationSetter iDestinationSetter;


    HealthZombi healthZombi;

    ZombieState activeState;
    enum ZombieState
    {
        STAND,
        MOVE,
        ATTACK
    }


    void Awake()
    {

        animator = GetComponent<Animator>();

        aIPath = GetComponent<AIPath>();
        iDestinationSetter = GetComponent<AIDestinationSetter>();

        healthZombi = GetComponent<HealthZombi>();
    }
    void Start()
    {

        player = FindObjectOfType<Player>();
        playerLife = FindObjectOfType<PlayerLife>();
        activeState = ZombieState.STAND;
    }

    void Update()
    {
        if (!healthZombi.isDeath)
        {
            WhatState();
        }

    }

    void WhatState()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        RayScan();

        switch (activeState)
        {
            case ZombieState.STAND:
                if (RayScan())
                {
                  
                    activeState = ZombieState.MOVE;
                    return;
                }

                if (imPatrol)
                {
                    Chill();
                    animator.SetFloat("Speed", 1);
                }
                else
                {
                    animator.SetFloat("Speed", 0);
                }
                aIPath.enabled = false;
                break;

            case ZombieState.MOVE:
                if (distance < attackRadius)
                {
                    activeState = ZombieState.ATTACK;
                    return;
                }

                if (distance > escapeRadius || !RayScan())
                {
                    activeState = ZombieState.STAND;
                }
                animator.SetFloat("Speed", 1);
                aIPath.enabled = true;
                iDestinationSetter.target = player.transform;
                break;


            case ZombieState.ATTACK:
                if (distance > attackRadius)
                {
                    activeState = ZombieState.MOVE;
                    return;
                }
                animator.SetFloat("Speed", 0);
                aIPath.enabled = false;
                AttackZombi();

                break;
        }
    }

    void Chill()
    {
        if (goBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[0].position, speed * Time.deltaTime);
            transform.up = (moveSpots[0].position - transform.position) * -1;

            if (Vector2.Distance(transform.position, moveSpots[0].position) <= 0.1f)
                goBack = false;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[1].position, speed * Time.deltaTime);
            transform.up = (moveSpots[1].position - transform.position) * -1;

            if (Vector2.Distance(transform.position, moveSpots[1].position) <= 0.1f)
                goBack = true;
        }
    }
    void AttackZombi()
    {
        if (nextAttack <= 0)
        {
            animator.SetTrigger("Shoot");
            nextAttack = attackTime;
        }
        if (nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
        }
    }
    public void DamageEvent()
    {
        playerLife.Damage(damageAttack);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, moveRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, escapeRadius);
    }


    bool GetRays(Vector2 dir, int distance)
    {
        LayerMask asphalt = LayerMask.GetMask("Asphalt");
        LayerMask player = LayerMask.GetMask("Player");

        if (Physics2D.Raycast(transform.position, dir, distance, asphalt))
        {
            Debug.DrawRay(transform.position, dir * distance, Color.blue);
            result = false;
        }

        else if (Physics2D.Raycast(transform.position, dir, distance, player))
        {
            result = true;
            Debug.DrawRay(transform.position, dir * distance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, dir * distance, Color.red);
            result = false;
        }

        return result;
    }

    bool RayCast(int rayAmt, float j, float angle, int distance)
    {
        bool a = false;
        bool b = false;

        for (int i = 0; i < rayAmt; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += angle * Mathf.Deg2Rad / rayAmt;

            Vector2 dir = -transform.TransformDirection(new Vector2(x, y));

            if (GetRays(dir, distance)) a = true;

            if (x != 0)
            {
                dir = -transform.TransformDirection(new Vector2(-x, y));
                if (GetRays(dir, distance)) b = true;
            }
        }
        if (a || b)
        {
            result = true;
        }

        return result;
    }

    bool RayScan()
    {
        int raysOne = 10;
        int raysTwo = 120;

        float angleOne = 30;
        float angleTwo = 360;

        float j = 0;

        if (RayCast(raysOne, j, angleOne, 15) || RayCast(raysTwo, j, angleTwo, 5))
        {
            result = true;
        }

        return result;
    }
}