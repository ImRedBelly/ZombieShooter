using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;


    public Text goInTank;
    public bool insideTank = false;

    Rigidbody2D rb;
    Animator animator;
    PlayerLife playerLife;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerLife = GetComponent<PlayerLife>();
    }
    void Start()
    {
        playerLife.ImDead += Dead;
    }
    void Update()
    {
        if (playerLife.imLife && !insideTank)
        {
            Move();
            Rotate();
        }



        if (goInTank.gameObject.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            insideTank = true;
            gameObject.SetActive(false);
            print("inside");
        }
        


    }

    void Dead()
    {
        animator.SetTrigger("Death");
        rb.velocity = Vector2.zero;
    }
    void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(inputX, inputY);
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }

        animator.SetFloat("Speed", direction.magnitude);
        rb.velocity = direction.normalized * speed;
    }
    void Rotate()
    {
        Vector3 playerPosotion = transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - playerPosotion;
        direction.z = 0;
        transform.up = -direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            goInTank.gameObject.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tank"))
        {
            goInTank.gameObject.SetActive(false);
        }
    }
}
