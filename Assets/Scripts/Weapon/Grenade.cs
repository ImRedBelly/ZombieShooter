using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grenade : MonoBehaviour
{
    public float explosiveRadius = 7;
    public int explosiveDamage = 5;

    public LayerMask damageLayer;

    public Animator animator;
    public GameObject explosive;

    void Start()
    {
        StartCoroutine(Explode(2));
    }

    IEnumerator Explode(float time)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(-transform.up * 15, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1.2f);

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(time);

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
