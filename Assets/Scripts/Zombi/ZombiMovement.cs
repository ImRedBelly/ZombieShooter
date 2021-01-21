using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombiMovement : MonoBehaviour
{
    public float speed = 5;

    Player player;
    Rigidbody2D rb;
    Animator animator;
    HealthZombi healthZombi;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthZombi = GetComponent<HealthZombi>();
    }
    void Start()
    {
        player = Player.Instance;
    }

    void Update()
    {
        if (!healthZombi.isDeath)
        {
            Move();
            Rotate();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    void Move()
    {

        Vector3 zombiPosition = transform.position;
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = playerPosition - zombiPosition;

        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }
        animator.SetFloat("Speed", direction.magnitude);
        rb.velocity = direction.normalized * speed;
    }
    void Rotate()
    {
        Vector3 zombiPosition = transform.position;
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = playerPosition - zombiPosition;

        direction.z = 0;
        transform.up = -direction;
    }

    void OnDisable()
    {
        rb.velocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
    }
}
