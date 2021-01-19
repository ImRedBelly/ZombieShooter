using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveTank : MonoBehaviour
{
    public GameObject gunTank;
    public GameObject gunPosition;
    public Animator gunPositionAnimator;
    public GameObject bullet;
    PlayerMovement playerMovement;
    Rigidbody2D rb;
    Vector2 positionY;


    public float fireRate = 1f;
    float nextFire;

    float rotationZ = 90;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gunPositionAnimator = gunPosition.GetComponent<Animator>();
    }
    void Start()
    {
        positionY = transform.position;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }


    void Update()
    {
        if (playerMovement.insideTank)
        {
            Move();
            RotateTank();
            RotateGun();

            if (Input.GetButton("Fire1") && nextFire <= 0)
            {
                Shot();
            }
            if (nextFire > 0)
            {
                nextFire -= Time.deltaTime;
            }
        }
        if (playerMovement.insideTank && Input.GetKeyDown(KeyCode.F))
        {
            playerMovement.insideTank = false;
            playerMovement.gameObject.SetActive(true);
            print("no inside");
        }
    }
    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.right * 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector2.right * 10 * Time.deltaTime);
        }
    }
    void RotateTank()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rotationZ -= 25f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rotationZ += 25f * Time.deltaTime;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    void RotateGun()
    {
        Vector3 gunPosotion = gunTank.transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - gunPosotion;
        direction.z = 0;
        gunTank.transform.up = -direction;
    }
    void Shot()
    {
        LeanPool.Spawn(bullet, gunPosition.transform.position, gunTank.transform.rotation);

        gunPositionAnimator.SetTrigger("Shot");
        nextFire = fireRate;
    }
}
