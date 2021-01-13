using System;
using UnityEngine;


public class PlayerLife : MonoBehaviour
{
    public HealthBar healthBar;

    public Action ImDead = delegate { };
    public Action HealthChange = delegate { };
    
    public bool imLife;
    
    int health = 10;
    int minHealth = 0;
    int maxHealth = 10;

    
    public int Life
    {
        get
        {
            return health;
        }

        set
        {
            if (value <= minHealth)
            {
                health = minHealth;
                ImDead();
            }
            else if (value >= maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health = value;
            }
            
        }
    }

    void Start()
    {
        healthBar.SetMaxHealth(health);
        HealthChange += ActionHeath;
        ImDead += Death;
    }

    public void Damage(int damagePoint)
    {
        Life -= damagePoint;
        HealthChange();
    }
    public void Healing(int healthPoint)
    {
        Life += healthPoint;
        HealthChange();
    }
    void Death()
    {
        imLife = false;
    }





    void ActionHeath()
    {
        healthBar.SetHealth(health);
    }
}
