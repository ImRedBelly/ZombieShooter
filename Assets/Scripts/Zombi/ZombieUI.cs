using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieUI : MonoBehaviour
{
    public Slider healthSlider;
    public HealthZombi healthZombi;
    void Start()
    {
        healthSlider.maxValue = healthZombi.health;
    }
    void Update()
    {
        healthSlider.value = healthZombi.health;
    }
}
