using UnityEngine;

public class Puddle : MonoBehaviour
{
    public PlayerMovement player;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        player.speed = 3;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.speed = 10;
    }
}
