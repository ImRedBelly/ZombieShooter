using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject hotBar;

    public PlayerLife playerLife;
    public GameObject deadScreen;

    public Text youDied;

    Color col;
    float a = 0;

    bool isPlayed;
    void Start()
    {
        col = youDied.color;
        deadScreen.SetActive(false);
        playerLife.ImDead += StartResrtart;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            hotBar.SetActive(true);
        }
        else
        {
            hotBar.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPlayed)
            {
                Play(1f, false);
            }
            else
            {
                Play(0, true);
            }
        }
    }

    void StartResrtart()
    {
        StartCoroutine(Restart());
    }
    public IEnumerator Restart()
    {
        deadScreen.SetActive(true);

 
        while (a < 1)
        {
            a += 0.02f;

            col.a = a;
            youDied.color = col;
            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
    public void Play(float pause, bool activ)
    {
        Time.timeScale = pause;
        isPlayed = activ;
    }
}
