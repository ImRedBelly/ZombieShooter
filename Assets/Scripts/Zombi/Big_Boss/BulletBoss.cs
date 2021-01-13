using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    float speed = 30f;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.velocity = -transform.up * speed;
        Destroy(gameObject, 5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLife playerLife = FindObjectOfType<PlayerLife>();
            playerLife.Damage(2);
        }
    }
}
