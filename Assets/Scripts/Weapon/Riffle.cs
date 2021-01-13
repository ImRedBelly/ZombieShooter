using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Riffle : MonoBehaviour
{
    public Bullet bullet;
    public GameObject riffle;
    public Text countBullet;

    public int bullets;
    public int bonusBullets = 5;

    public float fireRate = 1f;
    float nextFire;

    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        countBullet.text = "x " + bullets;
    }
    public void CheckFire()
    {

        if (Input.GetButton("Fire1") && nextFire <= 0)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; 
            }


            if (bullets > 0)
            {
                Shoot();
            }
            else
            {
                print("Нету потронов");
            }
        }


        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
    }


    public void Shoot()
    {
        bullets--;
        countBullet.text = "x " + bullets;
        animator.SetTrigger("Shoot");
        Instantiate(bullet, riffle.transform.position, transform.rotation);
        nextFire = fireRate;
    }

    public void AddBullet()
    {
        bullets += bonusBullets;
        countBullet.text = "x " + bullets;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MagazineRiffle"))
        {
            AddBullet();
        }
    }
}

