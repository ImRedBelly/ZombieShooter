using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public LayerMask damageLayer;
    public Animator animator;
    public GameObject explosive;

    public int explosiveDamage = 5;

    public float explosiveRadius = 7;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Explode(); 
        }
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosiveRadius, damageLayer);
        animator.SetTrigger("Boom");
        foreach (Collider2D coll in colliders)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                PlayerLife playerLife = FindObjectOfType<PlayerLife>();
                playerLife.Damage(explosiveDamage);
            }
            if (coll.gameObject.CompareTag("Zombi"))
            {
                HealthZombi zombi = coll.GetComponent<HealthZombi>();
                zombi.TakeDamage(explosiveDamage);
            }
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosiveRadius);
    }
}
