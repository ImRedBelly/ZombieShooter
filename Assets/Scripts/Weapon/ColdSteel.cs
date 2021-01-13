using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColdSteel : MonoBehaviour
{
    bool result;
    Animator animator;
    HealthZombi healthZombi;

    public float fireRate = 1f;
    float nextFire;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Shotknife()
    {
        RayScan();
        
        if (Input.GetButtonDown("Fire1") && nextFire <= 0)
        {

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            animator.SetTrigger("Shoot");

            if (RayScan())
            {
                nextFire = fireRate;
                healthZombi.DamageColdStell();
            }
            
        }
        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
    }


    bool GetRays(Vector2 dir, int distance)  // это выглядит ужасно
    {
        LayerMask zombi = LayerMask.GetMask("Zombi");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, zombi);
        if (hit)
        {
            if (hit.collider.tag == "Zombi")
            {
                healthZombi = hit.transform.GetComponent<HealthZombi>();
                Debug.DrawRay(transform.position, dir * distance, Color.blue);
                result = true;
            }
        }

        else
        {
            Debug.DrawRay(transform.position, dir * distance, Color.red);
            result = false;
        }
        return result;
    }

    bool RayCast(int rayAmt, float j, float angle, int distance)
    {
        bool a = false;
        bool b = false;

        for (int i = 0; i < rayAmt; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);

            j += angle * Mathf.Deg2Rad / rayAmt;

            Vector2 dir = -transform.TransformDirection(new Vector2(x, y));

            if (GetRays(dir, distance)) a = true;

            if (x != 0)
            {
                dir = -transform.TransformDirection(new Vector2(-x, y));
                if (GetRays(dir, distance)) b = true;
            }
        }
        if (a || b)
        {
            result = true;
        }

        return result;
    }

    bool RayScan()
    {
        int raysOne = 15;
        float angleOne = 70;
        float j = 0;

        if (RayCast(raysOne, j, angleOne, 7))
        {
            result = true;
        }

        return result;
    }
}
