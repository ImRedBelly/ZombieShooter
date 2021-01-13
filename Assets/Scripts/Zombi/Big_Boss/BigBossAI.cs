using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossAI : MonoBehaviour
{
    [Header("Radius Settings")]
    public float moveRadius = 10;
    public float escapeRadius = 15;

    public float attackRadiusOne = 3;

    public float attackRadiusTwo = 3;
    public float attackRadiusTwoTwo = 3;

    public float attackRadiusThree = 3;
    public float attackRadiusThreeThree = 3;

    public float distance;

    [Header("Attack Settings")]
    public GameObject bulletOne;
    public GameObject bulletTwo;

    public int damageAttack = 1;
    public float attackTime = 2f;
    float nextAttack;

    bool isAttack = true;


    Player player;
    PlayerLife playerLife;
    Animator animator;
    ZombiMovement movementScript;
    HealthZombi healthZombi;

    ZombieState activeState;
    enum ZombieState
    {
        STAND,
        MOVE,
        ATTACK_1,
        ATTACK_2,
        ATTACK_3,
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        movementScript = GetComponent<ZombiMovement>();
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
        Vector3 directoinToPlayer = player.transform.position - transform.position;
        float distance = Vector3.Distance(transform.position, player.transform.position);

        LayerMask layerMask = LayerMask.GetMask("Asphalt");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directoinToPlayer, directoinToPlayer.magnitude, layerMask);
        if(hit.collider != null)
        {
            return;
        }
        else
        {
            //нет объектов на пути
        }

        switch (activeState)
        {
            case ZombieState.STAND:

                Debug.DrawRay(transform.position, directoinToPlayer, Color.red);


                if (distance < moveRadius)
                {
                    activeState = ZombieState.MOVE;
                    return;
                }


                movementScript.enabled = false;
                break;



            case ZombieState.MOVE:
                if (distance < attackRadiusOne)
                {
                    activeState = ZombieState.ATTACK_1;
                    return;
                }
                if (distance < attackRadiusTwo && distance > attackRadiusTwoTwo)
                {
                    activeState = ZombieState.ATTACK_2;
                    return;
                }

                if (distance < attackRadiusThreeThree && distance > attackRadiusThree)
                {
                    activeState = ZombieState.ATTACK_3;
                    return;
                }

                if (distance > escapeRadius)
                {
                    activeState = ZombieState.STAND;
                }
                movementScript.enabled = true;
                break;


            case ZombieState.ATTACK_1:
                if (distance > attackRadiusOne)
                {
                    activeState = ZombieState.MOVE;
                    return;
                }
                movementScript.enabled = false;
                AttackZombi();
                break;

            case ZombieState.ATTACK_2:
                if (distance > attackRadiusTwo || distance < attackRadiusTwoTwo)
                {
                    activeState = ZombieState.MOVE;
                    return;
                }
                if (distance < attackRadiusOne)
                {
                    activeState = ZombieState.ATTACK_1;
                    return;
                }

                Shoot(bulletTwo);
                movementScript.enabled = true;
                break;

            case ZombieState.ATTACK_3:
                if (distance > attackRadiusThreeThree || distance < attackRadiusThree)
                {
                    activeState = ZombieState.MOVE;
                    return;
                }

                Shoot(bulletOne);
                movementScript.enabled = true;
                break;
        }
    }

    void Shoot(GameObject bullet)
    {
        if (nextAttack <= 0)
        {
            animator.SetTrigger("Attack_3");
            Instantiate(bullet, transform.position, transform.rotation);
            nextAttack = attackTime;
        }
        if (nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
        }
    }
    void AttackZombi()
    {

        if (isAttack)
        {
            if (nextAttack <= 0)
            {
                animator.SetTrigger("Attack_1");

                nextAttack = attackTime;
            }
            isAttack = false;
        }
        else
        {
            if (nextAttack <= 0)
            {
                animator.SetTrigger("Attack_2");

                nextAttack = attackTime;
            }
            isAttack = true;
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
        Gizmos.DrawWireSphere(transform.position, attackRadiusOne);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRadiusTwo);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRadiusTwoTwo);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRadiusThree);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRadiusThreeThree);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, escapeRadius);
    }
}