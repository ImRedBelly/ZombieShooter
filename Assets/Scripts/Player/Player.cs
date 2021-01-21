using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Lean.Pool;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public bool isMan;
    public bool isWoman;

    public GameObject grenade;
    public GameObject background;

    public int grenades = 5;
    public Text grenadeCount;

    public static bool fireBlocked = true;

    Gun gun;
    Riffle riffle;
    Animator animator;
    ColdSteel coldSteel;
    PlayerLife playerLife;

    public AnimatorOverrideController[] animatorsMan;
    public AnimatorOverrideController[] animatorsWoman;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;


        gun = GetComponent<Gun>();
        riffle = GetComponent<Riffle>();
        animator = GetComponent<Animator>();
        coldSteel = GetComponent<ColdSteel>();
        playerLife = GetComponent<PlayerLife>();
    }
    void Start()
    {
        grenadeCount.text = "x " + grenades;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && grenades > 0)
        {
            GrenadeThrow();
        }


        if (isMan)
        {
            GunMan();
            RiffleMan();
            KnifeMan();
            BatMan();
        }

        if (isWoman)
        {
            GunWoman();
            RiffleWoman();
            KnifeWoman();
            BatWoman();
        }
    }

    void GrenadeThrow()
    {
        grenades--;
        LeanPool.Spawn(grenade, transform.position, transform.rotation);
        grenadeCount.text = "x " + grenades;
    }

    public void WomanSkin(int indexSkin)
    {
        if (isWoman)
            animator.runtimeAnimatorController = animatorsWoman[indexSkin];
        if (isMan)
            animator.runtimeAnimatorController = animatorsMan[indexSkin];
    }

    void GunWoman()
    {
        if (animator.runtimeAnimatorController == animatorsWoman[1] && playerLife.imLife)
            gun.CheckFire();
    }
    void RiffleWoman()
    {
        if (animator.runtimeAnimatorController == animatorsWoman[3] && playerLife.imLife)
            riffle.CheckFire();
    }
    void KnifeWoman()
    {
        if (animator.runtimeAnimatorController == animatorsWoman[2] && playerLife.imLife)
            coldSteel.Shotknife();
    }
    void BatWoman()
    {
        if (animator.runtimeAnimatorController == animatorsWoman[0] && playerLife.imLife)
            coldSteel.Shotknife();
    }


    void GunMan()
    {
        if (animator.runtimeAnimatorController == animatorsMan[1] && playerLife.imLife)
            gun.CheckFire();
    }
    void RiffleMan()
    {
        if (animator.runtimeAnimatorController == animatorsMan[3] && playerLife.imLife)
            riffle.CheckFire();
    }
    void KnifeMan()
    {
        if (animator.runtimeAnimatorController == animatorsMan[2] && playerLife.imLife)
            coldSteel.Shotknife();
    }
    void BatMan()
    {
        if (animator.runtimeAnimatorController == animatorsMan[0] && playerLife.imLife)
            coldSteel.Shotknife();
    }


    public void Woman()
    {
        isWoman = true;
        background.SetActive(false);
        animator.runtimeAnimatorController = animatorsWoman[2];
    }
    public void Man()
    {
        isMan = true;
        background.SetActive(false);
        animator.runtimeAnimatorController = animatorsMan[2];
    }
    private void OnDestroy()
    {
        if(this == Instance)
        {
            Instance = null;
        }
    }
}
