using UnityEngine;

public class FirstAid : MonoBehaviour
{
    public int heathPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLife playerLife = FindObjectOfType<PlayerLife>();
            playerLife.Healing(heathPoint);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
