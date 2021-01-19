using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rb.velocity = - transform.up * speed;
    }

    private void OnBecameInvisible()
    {
        LeanPool.Despawn(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
            LeanPool.Despawn(gameObject);
        }
    }
}
