using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthZombi : MonoBehaviour
{
    public Action HealthChange = delegate { };
    public Image heathBar;

    public int health;
    public bool isDeath;

    float percentOfDamage;
    public int Life
    {
        get
        {
            return health;
        }
        set
        {
            if (value <= 0)
            {
                Death();
            }
            health = value;
        }
    }
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        heathBar.fillAmount = 1.0f;
        percentOfDamage = 1.0f / health;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }
    public void TakeDamage(int damage)
    {
        Life -= damage;
        heathBar.fillAmount -= percentOfDamage * damage;
    }
    void Death()
    {
        isDeath = true;
        animator.SetTrigger("Death");
        Destroy(transform.parent.gameObject, 3);
    }




    public void DamageColdStell()
    {
        Life -= 1;
        heathBar.fillAmount -= percentOfDamage * 1;
    }



    public void TakeHealing(int healthPoint)
    {
        Life += healthPoint; // для лечащих зомби
    }


}
