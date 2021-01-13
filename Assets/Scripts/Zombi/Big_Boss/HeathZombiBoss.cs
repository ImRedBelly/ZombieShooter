using UnityEngine;
using UnityEngine.UI;

public class HeathZombiBoss : MonoBehaviour
{
    public Slider slider;

    HealthZombi healthZombi;

    void Awake()
    {
        healthZombi = GetComponent<HealthZombi>();
    }
    private void Start()
    {
        healthZombi.HealthChange += Pr;
    }
    void Update()
    {
        slider.value = healthZombi.health;
    }
    private void Pr()
    {
        print("You");
    }
}
